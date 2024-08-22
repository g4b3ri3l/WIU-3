using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcceManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup audiomix; 
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.outputAudioMixerGroup = audiomix;
        audioSource.clip = BGM;
        audioSource.Play();
    }


}
