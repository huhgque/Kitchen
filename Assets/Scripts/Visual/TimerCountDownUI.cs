using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start() {
        GameManager.Instance.OnGameTimerUpdate += OnGameTimerUpdate;
    }
    void OnGameTimerUpdate(object sender,GameManager.OnGameTimerUpdateArgs onGameTimerUpdateArgs){
        int minute = Math.Max(0, (int) onGameTimerUpdateArgs.minute );
        int second = Math.Max(0, (int) onGameTimerUpdateArgs.second );
        text.text = minute + ":" + second;
    }
}
