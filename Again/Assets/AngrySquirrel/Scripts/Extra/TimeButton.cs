using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButton : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
        SoundManger.Instance.StopBGM(SoundManger.EBGM.BGM_Title);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        SoundManger.Instance.PlayBGM(SoundManger.EBGM.BGM_Title);
    }
}
