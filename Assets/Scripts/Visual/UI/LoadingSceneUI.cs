using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneUI : MonoBehaviour
{
    public static LoadingSceneUI Instance { get; private set; }
    [SerializeField] Image bg;
    [SerializeField] float fadeTime = 1f;
    bool isTransitioning = false;
    float count = 0f;
    float fromAlpha = 0f;
    float targetAlpha = 1f;
    void Awake() {
        Instance = this;
    }
    void Start() {
        DeactiveLoadingScene();
    }
    void Update() {
        if (!isTransitioning) return;
        count += Time.deltaTime;
        float delta = count/fadeTime;
        float progress = Mathf.Lerp(fromAlpha,targetAlpha,delta);
        bg.color = new Color(bg.color.r,bg.color.g,bg.color.b,progress);
    }
    public void ActiveLoadingScene(){
        isTransitioning = true;
        fromAlpha = 0f;
        targetAlpha = 1f;
    }
    public void DeactiveLoadingScene(){
        isTransitioning = true;
        fromAlpha = 1f;
        targetAlpha = 0f;

    }
    
}
