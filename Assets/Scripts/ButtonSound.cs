using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonSound : MonoBehaviour
{ 
    [SerializeField] AudioMixerGroup audioMixer;
    [SerializeField] AudioSource audiosource;
    public void ButtonPressedSound(AudioClip audioClip)
    {
        audiosource.outputAudioMixerGroup = audioMixer;
        audiosource.PlayOneShot(audioClip);
    }
}
