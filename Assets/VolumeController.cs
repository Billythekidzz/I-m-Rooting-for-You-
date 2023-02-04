using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public enum VolumeType { MASTER, SFX, MUSIC }

    [SerializeField]
    VolumeType volumeType = VolumeType.SFX;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    TextMeshProUGUI label;

    private void Start()
    {
        float volumeValueToSet = AudioManager.Instance.sfxVolume;
        if(volumeType == VolumeType.MASTER)
        {
            volumeValueToSet = AudioManager.Instance.masterVolume;
        }
        else if(volumeType == VolumeType.MUSIC)
        {
            volumeValueToSet = AudioManager.Instance.musicVolume;
        }
        label.text = Math.Round(volumeValueToSet * 100) + "%";
        volumeSlider.SetValueWithoutNotify(volumeValueToSet); 
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }

    private void OnVolumeSliderValueChanged(float arg0)
    {
        if (volumeType == VolumeType.SFX)
        {
            AudioManager.Instance.SetSFXVolume(arg0);
        }
        else if (volumeType == VolumeType.MUSIC)
        {
            AudioManager.Instance.SetMusicVolume(arg0);
        }
        else if (volumeType == VolumeType.MASTER)
        {
            AudioManager.Instance.SetMasterVolume(arg0);
        }
        label.text = Math.Round(arg0 * 100) + "%";
    }
}
