using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput playerInput {get;private set;}

    public event EventHandler OnInteraction;
    public event EventHandler OnUse;
    private PlayerAction playerAction;
    private void Awake(){
        playerAction = new PlayerAction();
        playerAction.Player.Enable();
        playerAction.Player.Interact.performed +=  PerformInteract;
        playerAction.Player.Use.performed += PerformUse;
        playerInput = this;
    }

    private void PerformInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteraction?.Invoke(this,EventArgs.Empty);
    }
    private void PerformUse(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnUse?.Invoke(this,EventArgs.Empty);
    }
    public Vector2 GetPlayerMovementNormalized(){
        Vector2 input = playerAction.Player.Move.ReadValue<Vector2>();
        return input.normalized;
    }
}
