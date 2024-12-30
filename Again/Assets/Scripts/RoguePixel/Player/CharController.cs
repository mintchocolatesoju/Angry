using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    InputAction moveInput;
    InputAction jumpInput;
    InputAction attackInput;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb2d;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 3f;

    public bool Grounded = true;
    public bool readytoAttack = true;
    
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.InputSystem.PlayerInput input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        moveInput = input.actions["Move"];
        jumpInput = input.actions["Jump"];
        attackInput = input.actions["Attack"];
        /*
        PlayerInput playerInput = GetComponent<PlayerInput>();
        moveInput = playerInput.Player.Move;
        jumpInput = playerInput.Player.Jump;
        */
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 moveValue = moveInput.ReadValue<Vector2>();
        
        if(moveValue.x !=0)
            spriteRenderer.flipX = moveValue.x < 0;
        
        animator.SetFloat("Speed", Math.Abs(moveValue.x));
        rb2d.velocity = new Vector2(moveValue.x * moveSpeed, rb2d.velocity.y);
       
        
    }

    private void Update()
    {
        if (jumpInput.triggered && Grounded)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log(Grounded);
            animator.Play("Rogue_Jump");
        }
        if (attackInput.triggered && readytoAttack )
        {
          
            BasicAttack();
            
        }
    }

    void BasicAttack()
    {
        animator.Play("Rogue_Attack");
        animator.SetTrigger("Idle");
       

    }

   
    
}
