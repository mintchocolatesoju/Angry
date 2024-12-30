using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance { get; private set; }
    public bool isBgmMuted { get; private set; } = false;

    public enum EBGM
    {
        BGM_Title,
       
    }

    public enum ESFX
    {
        SFX_Jump,
        SFX_Land,
        SFX_Collide,
        SFX_Pickup,
        SFX_StageClear,
        SFX_Death,
        SFX_WoodDestory,
        SFX_MonsterDie,
        SFX_Menu,
        SFX_Crouch,
    }
    
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    
    
    [Range(0f, 1f)] 
    public float bgmVolume = 0.3f;
    [Range(0f, 1f)]
    public float sfxVolume = 0.3f;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgmSource.loop = true;
        PlayBGM(EBGM.BGM_Title);
    }
    
    public void PlayBGM(EBGM bgm)
    {
        bgmSource.clip = bgms[(int)bgm];
        bgmSource.Play();
    }

    public void StopBGM(EBGM bgm)
    {
        bgmSource.Stop();
    }

    public void ToggleBGM()
    {
        isBgmMuted = !isBgmMuted;
        bgmSource.mute = isBgmMuted;
    }

    public void PlaySFX(ESFX sfx)
    {
        sfxSource.PlayOneShot(sfxs[(int)sfx], sfxVolume);
    }
    void Update()
    {
        // Inspector에서 bgmVolume 변경 시 동기화
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
    }
   
    
}
