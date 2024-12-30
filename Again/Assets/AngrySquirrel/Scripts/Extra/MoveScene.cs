using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public string sceneName;
    public void SceneMove()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
