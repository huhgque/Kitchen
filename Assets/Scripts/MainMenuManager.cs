using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play(){
        StartCoroutine(LoadSceneAsync(1));
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
