using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartUI : MonoBehaviour
{
    [SerializeField] Image heartTemplate;
    List<GameObject> heartList = new ();
    void Start() {
        GameManager.Instance.OnPlayerHeartChange += OnPlayerHeart;
    }
    private void OnPlayerHeart(object sender, GameManager.OnPlayerHeartChangeEventArgs e)
    {
        foreach (GameObject heart in heartList){
            Destroy(heart);
        }
        for (int i = 0;i<e.heart;i++){
            GameObject heart = Instantiate(heartTemplate.gameObject,gameObject.transform);
            heart.SetActive(true);
            heartList.Add(heart);
        }
    }
}
