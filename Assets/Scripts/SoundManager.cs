using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get;private set;}
    [SerializeField] private SoundRefSO soundRefSO;
    [SerializeField] private SoundLoopComponent stoveSound;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        ClearCounter.OnAnyItemPlace += OnAnyItemPlace;
        ContainerCounter.OnAnyContainerOpen += OnAnyContainerOpen;
        CuttingCounter.OnAnyCutting += OnAnyCutting;
        DeliverCounter.OnAnyDeliverySuccess += OnAnyDeliverySuccess;
        DeliverCounter.OnAnyDeliveryFail += OnAnyDeliveryFail;
        StoveCounter.OnAnyStoveOn += stoveSound.OnAudioPlay;
        StoveCounter.OnAnyStoveOff += stoveSound.OnAudioStop;
        TrashBin.OnAnyTrashed += OnAnyTrashed;
    }
    private void PlaySound(AudioClip[] audios,Vector3 position ,float volume = 0.5f){
        AudioClip audio = audios[UnityEngine.Random.Range(0,audios.Length)];
        AudioSource.PlayClipAtPoint(audio,position,volume);
    }
    private void OnAnyItemPlace(object sender,EventArgs args){
        PlaySound(soundRefSO.objectDrop,Camera.main.transform.position);
    }
    private void OnAnyContainerOpen(object sender,EventArgs args){
        PlaySound(soundRefSO.objectPickup,Camera.main.transform.position);
    }
    private void OnAnyDeliverySuccess(object sender,EventArgs args){
        PlaySound(soundRefSO.deliverySuccess,Camera.main.transform.position);

    }
    private void OnAnyDeliveryFail(object sender,EventArgs args){
        PlaySound(soundRefSO.deliveryFail,Camera.main.transform.position);

    }
    private void OnAnyCutting(object sender,EventArgs args){
        PlaySound(soundRefSO.chop,Camera.main.transform.position);

    }
    private void OnAnyStoveOn(object sender,EventArgs args){
        AudioClip[] audioClips = {soundRefSO.stoveSound};
        PlaySound(audioClips,Camera.main.transform.position);

    }
    private void OnAnyStoveOff(object sender,EventArgs args){

    }
    private void OnAnyTrashed(object sender,EventArgs args){
        PlaySound(soundRefSO.trash,Camera.main.transform.position);

    }
}
