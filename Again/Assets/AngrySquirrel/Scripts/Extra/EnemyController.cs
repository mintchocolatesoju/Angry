using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject fx;
    private Animator monsterAnimator;

    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
    }
   public void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.tag == "Player")
      {
          StartCoroutine(EnemyDestroy());
      }
   }

   IEnumerator EnemyDestroy()
   {
       SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_MonsterDie);
       yield return new WaitForSeconds(0.2f); // 여기다 음악소스 가져와서 붇이기
       if (fx != null)
       {
           Instantiate(fx, transform.position, Quaternion.identity);
           Destroy(gameObject);
       }
   }
    
}


