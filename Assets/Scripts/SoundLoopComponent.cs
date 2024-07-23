using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoopComponent : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnAudioPlay(object sender,EventArgs args){
        audioSource.Play();
    }
    public void OnAudioStop(object sender,EventArgs args){
        audioSource.Stop();
    }
}
