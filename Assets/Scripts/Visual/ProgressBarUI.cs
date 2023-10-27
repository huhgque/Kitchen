using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Image imageBar;
    private IProgressBarDisplay hasProgressbar;
    public void Start(){
        hasProgressbar = parent.GetComponent<IProgressBarDisplay>();
        hasProgressbar.OnProgressBarChange += HandleProgress;
        imageBar.fillAmount = 0f;
    }

    private void HandleProgress(object sender,IProgressBarDisplay.ProgressBarArgs args){
        imageBar.fillAmount = args.progress;
    }
    
}
