using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BaseCounter clearCounter;
    [SerializeField] private GameObject[] selectedCounterVisual;
    private Player player;
    void Start()
    {
        player = Player.Instance;
        player.OnSelectCounter += OnSelectedCounter;
    }
    private void OnSelectedCounter(object sender,Player.OnSelectCounterArgs s){
        if (clearCounter == s.selectedCounter){
            foreach (GameObject g in selectedCounterVisual){
                g.SetActive(true);
            }
        }else{
            foreach (GameObject g in selectedCounterVisual){
                g.SetActive(false);
            }
        }
    }
}
