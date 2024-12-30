using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [Header("Images")]
    public Sprite Image1;
    public Sprite Image2;

    [Header("components")] private Image buttonImage;
    
    public void Start()
    {
      buttonImage = GetComponent<Image>();
      buttonImage.sprite = Image1;
    }
    
    private void Mute()
    {
        SoundManger.Instance.ToggleBGM();
        spriteChange();
    }

    private void spriteChange()
    {
        if (SoundManger.Instance.isBgmMuted)
        {
            buttonImage.sprite = Image2;
        }
        else
        {
            buttonImage.sprite = Image1;
        }
    }
    
    
}
