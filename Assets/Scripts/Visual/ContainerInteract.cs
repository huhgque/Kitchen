using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteract : MonoBehaviour
{
    [SerializeField] private ContainerCounter container;
    [SerializeField] private GameObject sprite;
    Animator animator;
    public void Awake(){
        animator = GetComponent<Animator>();
    }
    public void Start(){
        container.OnContainerInteract += PlayInteractAnimation;
        sprite.GetComponent<SpriteRenderer>().sprite = container.GetKitchenSO().sprite;
    }
    private void PlayInteractAnimation(object sender,EventArgs args){
        animator.SetTrigger("OpenClose");
    }
}
