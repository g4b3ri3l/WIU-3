using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSettingsManager : MonoBehaviour, ISaveable
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private string masterVolumeName;
    
    
    private void Start()
    {
        Load();
        SetMasterVolume();
    }

    private void Update()
    {
        SetMasterVolume();
        Save();
    }

    public void SetMasterVolume()
    {
        // -40 is the minimum volume that can be heard
        // The following is to calculate the volume to be in the range of -40 to 0 based on the slider value
        float volume = (masterVolumeSlider.value*100 / 20 - 100);
        // Set the mixer master volume to the slider value
        mixer.SetFloat(masterVolumeName, volume);
    }
    public void Save()
    {
        float saveVolume = masterVolumeSlider.value;
        PlayerPrefs.SetFloat(masterVolumeName, saveVolume);
    }

    public void Load()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolumeName);
    }
}
