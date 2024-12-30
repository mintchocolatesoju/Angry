using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    
    public GameObject fx;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(BlockDestroy());
        }
    }

    IEnumerator BlockDestroy()
    {
        SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_WoodDestory);
        yield return new WaitForSeconds(0.1f); // 여기다 음악소스 가져와서 붇이기
        if (fx != null)
        {
            Instantiate(fx, transform.position, Quaternion.identity);
            
        }
        
        Destroy(gameObject);
    }
}
