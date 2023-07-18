using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    // ������ �����ϴ� ��
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
        if(controller.collisions.above || controller.collisions.below) // ���̳� õ�忡 ������
        {
            velocity.y = 0;
        }

        if (!isHit && !isStun)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // �Է°� �ޱ�
        }
        OnStun();
        FlipX();

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below && !isHit && !isStun) // ���� ���� ����� ���
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed; // �¿� ������ �ӵ� 
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
            Debug.Log("�¾Ҵ�!");
            isHit = true; // �Է��� ���´�.
            input = Vector2.zero; // ����Ǵ� �������� �����.
            controller.collisions.below = false; // ���߿� ����
            animator.SetBool("onHit", true); // ����� ����.
            velocity = new Vector2(-5, 5); // �ش�������� ������.
            StartCoroutine("WaitStunTime");
        }
    }
    public void OnStun()
    {
        if (controller.collisions.below && isHit) // ���� ������
        {
            isStun = true;
            isHit = false;
            Debug.Log("���� ��Ҵ�!");
            controller.collisions.below = true; // ���� �پ���.
            animator.SetBool("onHit", false); // ������ �����Ѵ�.
            isStun = false;
        }
    }

    IEnumerator WaitStunTime()
    {
        animator.SetBool("Hit",true);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        animator.SetBool("onStun", false); // ����� �����.
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
