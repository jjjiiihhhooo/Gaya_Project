using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player_Move : MonoBehaviour
{
    // 실제로 조절하는 값
    [Header("-------------------------실제로 변경하는 값------------------------- ")]
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float moveSpeed = 6;

    float gravity;
    float jumpVelocity;

    [Header("-------------------------결과 값------------------------- ")]
    public Vector3 velocity;
    public Vector2 input;
    
    // 변수
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float velocityXSmoothing;
    public bool isStun;

    Controller2D controller;
    Animator animator;
    

    void Start()
    {
        isStun = false;
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity : " + gravity + "  Jump Velocity : " + jumpVelocity); 
    }

    public void UpdateStatus()
    {
        moveSpeed = PlayerStatus.Instance.Speed;
    }

    void Update()
    {
        if(controller.collisions.above || controller.collisions.below) // 땅이나 천장에 닿을때
        {
            velocity.y = 0;
        }

        if (!isStun)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // 입력값 받기
        }
        FlipX();

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below && !isStun) // 땅에 발이 닿았을 경우 입력을 받고
        {
            animator.SetBool("isJump", true);
            velocity.y = jumpVelocity;
        }
        else
        {
            animator.SetBool("isJump", false);
        }

        float targetVelocityX = input.x * moveSpeed; // 좌우 움직임 속도 
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *  Time.deltaTime);
    }

    private void FlipX()
    {
        if (input.x == 1)
        {
            animator.SetBool("isMove", true);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (input.x == -1)
        {
            animator.SetBool("isMove", true);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetBool("isMove", false);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
