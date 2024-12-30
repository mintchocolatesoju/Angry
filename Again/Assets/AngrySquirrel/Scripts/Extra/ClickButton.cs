using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickButton : MonoBehaviour
{
    public void click()
    {
        SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Menu);
    }
}
