using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    // Start is called before the first frame update
    private void Start() {
        counter.gameObject.SetActive(false);
        GameManager.Instance.OnGameReady += OnGameReady;
        GameManager.Instance.OnGameStart += OnGameStart;
    }
    private void OnGameReady(object sender , EventArgs args){
        counter.gameObject.SetActive(true);
    }
    private void OnGameStart(object sender, EventArgs args){
        counter.gameObject.SetActive(false);
    }
    private void Update() {
        counter.text = ((int)GameManager.Instance.GetTimeCountDown()).ToString();
    }
}
