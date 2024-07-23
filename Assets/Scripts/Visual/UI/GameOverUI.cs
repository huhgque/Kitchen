using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject[] uiObjects;
    void Awake() {
        foreach (var obj in uiObjects) {
            obj.SetActive(false);
        }
    }
    void Start() {
        GameManager.Instance.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(object sender, EventArgs e)
    {
        foreach (var obj in uiObjects) {
            obj.SetActive(true);
        }
    }
}
