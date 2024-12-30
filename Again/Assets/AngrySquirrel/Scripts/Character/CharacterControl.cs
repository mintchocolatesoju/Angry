using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;

public class CharacterControl : MonoBehaviour
{
    [Header("Components")] 
    public Camera mainCamera;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private GroundCheck groundCheck;
    [SerializeField] private LineRenderer lineRenderer;
    SpriteRenderer spriteRenderer;

    [Header("States")] 
    private bool canJump = true;
    private bool isDragging = false;
    private Vector2 startDragPosition;
    private Vector2 endDragPosition;
    
    [Header("Properties")] 
    public float jumpForce = 1.2f;
    
    [Header("CameraSettings")]
    [SerializeField] private Vector3 cameraOffset; // 카메라의 상대적 위치
    [SerializeField] private float cameraFollowSpeed = 5f; //카메라 따라가는 속도
    
    [Header("trajectory")]
    [SerializeField] private GameObject trajectoryPrefab;
    [SerializeField] private int maxTrajectory = 10;
    private readonly List<GameObject> trajectory = new List<GameObject>();

    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 메인 카메라 자동 할당
        }
    }

    private void OnMouseDown()
    {
        if(!canJump) return;
        startDragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Crouch);
        isDragging = true;
        //Crouch();

    }

    void OnMouseDrag()
    {
        if(!isDragging || !canJump) return;
        Crouch();
        Vector2 currentDragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (startDragPosition - currentDragPosition).normalized;
        float dragDistance = Vector2.Distance(startDragPosition, currentDragPosition);
        Vector2 velocity = direction * dragDistance * jumpForce/rigidbody.mass;
        //Debug.Log(currentDragPosition);
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = direction.x < 0; // x 값이 양수면 오른쪽으로 드래그, 음수면 왼쪽으로 드래그
        }
        
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startDragPosition);
            lineRenderer.SetPosition(1, currentDragPosition);
        }
        DrawTrajectory(startDragPosition, velocity);
    }
    /// <summary>
    /// 마우스 때고 작동
    /// </summary>
    private void OnMouseUp()
    {
        if(!isDragging) return;
        endDragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        isDragging = false;
        groundCheck.isGrounded = false;
        canJump = false;
        animator.Play("Jump");
        SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Jump);
        Vector2 direction= (startDragPosition - endDragPosition).normalized;
        float dragDistance = Vector2.Distance(startDragPosition, endDragPosition);
        rigidbody.AddForce(direction * jumpForce *dragDistance, ForceMode2D.Impulse);
        
        if (lineRenderer != null) 
            lineRenderer.positionCount = 0;
        ClearTrajectory();
    }
    
    /// <summary>
    ///  경로를 그리는 함수
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="velocity"></param>
    private void DrawTrajectory(Vector2 startPosition, Vector2 velocity)
    {
        Vector2 gravity = Physics2D.gravity;
        float timeStep = 0.05f;
        float totalDistance = 0f;
        
        Vector2 previousPosition = startPosition;
        for (int i = 0; i < maxTrajectory; i++)
        {
            float t = i*timeStep;
            Vector2 currentPosition = startPosition + velocity * t + 0.5f * gravity * t * t * rigidbody.gravityScale;

            if (i >= trajectory.Count)
            {
                GameObject newTrajectory = Instantiate(trajectoryPrefab, currentPosition, Quaternion.identity);
                trajectory.Add(newTrajectory);
            }
            else
            {
                trajectory[i].transform.position = currentPosition;
                trajectory[i].SetActive(true);
            }
            previousPosition = currentPosition;
        }

        for (int i = trajectory.Count - 1; i >= maxTrajectory; i--)
        {
            trajectory[i].SetActive(false);
        }
    }
    
    private void ClearTrajectory()
    {
        foreach (GameObject dot in trajectory)
        {
            dot.SetActive(false);
        }
    }
    
    void Crouch()
    {
        if (canJump)
        {
            animator.Play("Crouch");
           // SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Crouch);
            //IsInAnimation("Crouch");
            //Debug.Log("Crouch");
        }
            
    }

   public void Idle()
    {
        animator.Play("Idle");
        //SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Land);
    }

    private bool IsInAnimation(string animationName)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animationName);
    }
    
    void Update()
    {
        if (groundCheck.isGrounded && !isDragging)
        {
            canJump = true;
            Idle();
        }
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            Vector3 targetPosition = transform.position + cameraOffset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Blocks" || collision.gameObject.tag == "Enemy")
        SoundManger.Instance.PlaySFX(SoundManger.ESFX.SFX_Collide);
    }

    
}   
