using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    // 실제로 조절하는 값
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float moveSpeed = 6;

    float gravity;
    float jumpVelocity;
    public Vector3 velocity;
    Vector2 input;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float velocityXSmoothing;

    public bool isStun;
    public bool isHit;
    Controller2D controller;

    Animator animator;

    void Start()
    {
        isStun = false;
        isHit = false;
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity : " + gravity + "  Jump Velocity : " + jumpVelocity); 
    }

    void Update()
    {
        if(controller.collisions.above || controller.collisions.below) // 땅이나 천장에 닿을때
        {
            velocity.y = 0;
        }

        if (!isHit && !isStun)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // 입력값 받기
        }
        OnStun();
        FlipX();

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below && !isHit && !isStun) // 땅에 발이 닿았을 경우
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed; // 좌우 움직임 속도 
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *  Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHit(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnHit(collision);
    }

    public void OnHit(Collider2D collision)
    {
        if (collision.gameObject.layer == 7) // Enemy
        {
            Debug.Log("맞았다!");
            isHit = true; // 입력을 막는다.
            input = Vector2.zero; // 적용되던 움직임을 멈춘다.
            controller.collisions.below = false; // 공중에 띄운다
            animator.SetBool("onHit", true); // 모션을 띄운다.
            velocity = new Vector2(-5, 5); // 해당방향으로 날린다.
            StartCoroutine("WaitStunTime");
        }
    }
    public void OnStun()
    {
        if (controller.collisions.below && isHit) // 땅에 닿을때
        {
            isStun = true;
            isHit = false;
            Debug.Log("땅에 닿았다!");
            controller.collisions.below = true; // 땅에 붙었다.
            animator.SetBool("onHit", false); // 변수를 정리한다.
            isStun = false;
        }
    }

    IEnumerator WaitStunTime()
    {
        animator.SetBool("Hit",true);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        animator.SetBool("onStun", false); // 모션을 지운다.
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
