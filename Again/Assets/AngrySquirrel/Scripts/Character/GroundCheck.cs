using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("components")]
    private Animator parentAnimator;

   
    
    [Header("states")] 
    public bool isGrounded=true;

    public void Start()
    {
        parentAnimator = GetComponentInParent<Animator>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            isGrounded = true;
            parentAnimator.Play("Idle");
            SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Land);
            
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            isGrounded = false;
            
        }
    }
}
