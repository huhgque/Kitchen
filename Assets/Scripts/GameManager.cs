using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int MAIN_MENU_SCENE = 0;
    const int GAME_SCENE = 1;
    public static GameManager Instance {get;private set;}
    public event EventHandler<OnGameTimerUpdateArgs> OnGameTimerUpdate;
    public class OnGameTimerUpdateArgs: EventArgs{
        public int minute;
        public int second;
    }
    public event EventHandler OnGameReady;
    public event EventHandler OnGameStart;
    public event EventHandler OnGameEnd;
    public event EventHandler OnGameStateChange;
    public class OnGameStateChangeArgs : EventArgs{
        public GameState gameState;
    }
    public enum GameState{
        READY,
        PLAY,
        END
    }
    private GameState gameState;
    private float readyTimer = 6;
    [SerializeField] private float gamePLayTime = 60;
    [SerializeField] int heart = 5;
    public event EventHandler<OnPlayerHeartChangeEventArgs> OnPlayerHeartChange;
    public class OnPlayerHeartChangeEventArgs : EventArgs{
        public int heart;
    }
    private float timeCount = 0;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        StartGame();    
    }
    private void Update() {
        switch (gameState) {
            case GameState.READY :
                timeCount -= Time.deltaTime;
                if(timeCount < 0){
                    gameState = GameState.PLAY;
                    timeCount = gamePLayTime;
                    OnGameStart?.Invoke(this,EventArgs.Empty);
                }
                break;
            case GameState.PLAY :
                // timeCount -= Time.deltaTime;
                // OnGameTimerUpdate?.Invoke(this,new OnGameTimerUpdateArgs{minute = (int) Math.Floor(timeCount/60) , second = (int) timeCount%60 } );
                // if (timeCount < 0){
                //     gameState = GameState.END;
                //     timeCount = 0;
                //     OnGameEnd?.Invoke(this,EventArgs.Empty);
                // }
                break;
            case GameState.END :
                break;
        }
    }
    public void StartGame(){
        OnPlayerHeartChange?.Invoke(this,new OnPlayerHeartChangeEventArgs{ heart = heart });
        timeCount = readyTimer;
        gameState = GameState.READY;
        OnGameTimerUpdate?.Invoke(this,new OnGameTimerUpdateArgs{minute = (int) Math.Floor(timeCount/60) , second = (int) timeCount%60 } );
        OnGameReady?.Invoke(this,EventArgs.Empty);
    }
    public float GetTimeCountDown(){
        return timeCount;
    }
    public bool IsGamePlaying(){
        return gameState == GameState.PLAY;
    }
    public void AddBonusHeart(int heart){
        this.heart+=heart;
    }
    public void FailOrder(){
        heart -= 1;
        OnPlayerHeartChange?.Invoke(this,new OnPlayerHeartChangeEventArgs{ heart = heart });
        if (heart <= 0){
            gameState = GameState.END;
            timeCount = 0;
            OnGameEnd?.Invoke(this,EventArgs.Empty);
        }
    }
    public void Retry(){
        StartCoroutine(LoadSceneAsync(GAME_SCENE));
    }
    public void Exit(){
        StartCoroutine(LoadSceneAsync(MAIN_MENU_SCENE));
    }
    IEnumerator LoadSceneAsync(int sceneIndex){
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone){
            float progress = Mathf.Clamp01(asyncOperation.progress);
            Debug.Log(progress);
            yield return null;
        }
    }
}
