using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image countDown;
    float count = 0;
    float waitTime = 0;
    bool doCount = false;
    void Start() {
        if (countDown) countDown.fillAmount = 0;
    }
    void Update() {
        if (!doCount) return;
        count += Time.deltaTime;
        float progress = count / waitTime;
        countDown.fillAmount = progress;
    }
    public bool TryAddIcon(KitchenSO kitchenSO){
        icon.sprite = kitchenSO.sprite;
        return true;
    }
    public void SetWaitTime(float time){
        waitTime = time;
    }
    public void StartCountDown(){
        doCount = true;
    }
}
