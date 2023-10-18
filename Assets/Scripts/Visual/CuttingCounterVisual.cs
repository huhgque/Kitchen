using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter container;
    Animator animator;
    public void Awake(){
        animator = GetComponent<Animator>();
    }
    public void Start(){
        container.OnCutting += PlayInteractAnimation;
    }
    private void PlayInteractAnimation(object sender,EventArgs args){
        animator.SetTrigger("Cut");
    }
}
