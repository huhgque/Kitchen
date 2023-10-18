using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter parent;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject stoveOnEmitsion;
    private ParticleSystem particleSys;
    public void Start(){
        parent.OnStoveStateChange += HandleStoveStateChange;
        particleSys = particle.GetComponent<ParticleSystem>();
    }

    public void HandleStoveStateChange(object sender , StoveCounter.OnStoveStateChangeArgs args){
        switch (args.cookingState) {
            case StoveCounter.CookingState.IDLE:
                stoveOnEmitsion.SetActive(false);
                particle.SetActive(false);
                particleSys.Stop();
                break;
            case StoveCounter.CookingState.COOKING:
                stoveOnEmitsion.SetActive(true);
                particle.SetActive(true);
                particleSys.Play();
                break;
            case StoveCounter.CookingState.BURNED:
                particle.SetActive(false);
                particleSys.Stop();
                break;
        }
    }
}
