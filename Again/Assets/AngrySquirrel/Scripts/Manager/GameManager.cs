using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int acornCount = 0;
    public int targetCount = 3;
    public TMP_Text acornText;
    
    
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
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void AddAcorn()
    {
        acornCount++;
        Debug.Log(acornCount);
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        if (acornText != null)
        {
            acornText.text = $"Acorn: {acornCount}/{targetCount}";
        }
    }

    public void ReachGoal()
    {
        if (acornCount >= targetCount )
        {
            SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_StageClear);
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            return;
        }
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        //Debug.Log("Game is quitting...");
        Application.Quit();

        // 에디터에서 실행 시 동작 확인

    }
}
