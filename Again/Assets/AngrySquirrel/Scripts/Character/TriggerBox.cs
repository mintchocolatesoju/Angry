using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBox : MonoBehaviour
{
    [Header("components")]
    private Animator parentAnimator;
    

    void Start()
    {
        parentAnimator = GetComponentInParent<Animator>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (parentAnimator == null)
            return;
       
        if (other.gameObject.layer == 3)
        {
            //Debug.Log("Get Acorn");
            SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Pickup);
            GameManager.Instance.AddAcorn();
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 8)
        {
            SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Death);
            parentAnimator.Play("Die");
            GameManager.Instance.acornCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //아래로 떨어지기
            //게임오버창 띄우기
        }

        if (other.gameObject.layer == 9)
        {
            GameManager.Instance.ReachGoal();
            //SceneManager.LoadScene("MenuScene");
        }
        
    }
    // collider가 2개 동시에 닿으면 버그남 
    //raycast를 사용 고려..?
    
    
}
