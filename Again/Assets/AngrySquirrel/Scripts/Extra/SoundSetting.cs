using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;

    void onEnable()
    {
        InitializeSlider();
    }

    private void InitializeSlider()
    {
        if (SoundManger.Instance != null)
        {
            bgmSlider.value = SoundManger.Instance.bgmVolume;
        }
    }
    
    public void Start()
    {
        bgmSlider.onValueChanged.AddListener(value => SoundManger.Instance.bgmVolume = value);
        bgmSlider.value = SoundManger.Instance.bgmVolume;
    }
}
