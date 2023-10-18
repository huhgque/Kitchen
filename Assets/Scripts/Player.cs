using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IKitchenObjectParent
{   
    public static Player Instance {get;private set;}
    public event EventHandler<OnSelectCounterArgs> OnSelectCounter;
    public class OnSelectCounterArgs:EventArgs{
        public BaseCounter selectedCounter;
    }
    private bool isWalking = false;
    private Vector3 lastDirection ;
    private BaseCounter selected ;
    private KitchenObject kitchenObject;
    [SerializeField] private GameObject objectHoldPosition;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotateSpeed = 8f;
    [SerializeField] private PlayerInput input ;
    // Start is called before the first frame update
    void Awake(){
        if (!Instance)
            Instance = this;
    }
    void Start()
    {
        input.OnInteraction += Interaction;
        input.OnUse += Use;
    }

    void Interaction(object sender,EventArgs e){
        if (selected) {
            selected.Interact(this);
        }
    }
    private void Use(object sender,EventArgs e){
        if (selected){
            selected.Use(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteract();
    }

    public bool IsWalking(){
        return isWalking;
    }

    
    private void HandleInteract(){
        float reach = 1f;
        Vector2 movementVector = input.GetPlayerMovementNormalized();
        Vector3 direction = new Vector3(movementVector.x,0,movementVector.y);
        if(direction != Vector3.zero){
            lastDirection = direction;
        }
        bool didHit = Physics.Raycast(transform.position,lastDirection,out RaycastHit hitInfo,reach);
        if(didHit){
            if(hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter clearCounter )){
                if (selected != clearCounter){
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        }else{
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement(){
        float height = 2f;
        float radius = .4f;
        Vector2 movementVector = input.GetPlayerMovementNormalized();
        Vector3 direction = new Vector3(movementVector.x,0,movementVector.y);
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * height , radius , direction,radius);
        if (movementVector != Vector2.zero) isWalking = true;
        else isWalking = false;
        if(canMove)
            transform.position += direction * Time.deltaTime * speed;
        if(!canMove){
            Vector3 moveZ = new Vector3(0,0,direction.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * height , radius , moveZ,radius);
            if(canMove)
                transform.position += moveZ * Time.deltaTime * speed;
        }
        if(!canMove){
            Vector3 moveX = new Vector3(direction.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * height , radius , moveX,radius);
            if(canMove)
                transform.position += moveX * Time.deltaTime * speed;
        }
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter clearCounter){
        selected = clearCounter;
        OnSelectCounter?.Invoke(this,new OnSelectCounterArgs{selectedCounter = clearCounter});
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public GameObject GetObjectDisplayPosition()
    {
        return objectHoldPosition;
    }
}
