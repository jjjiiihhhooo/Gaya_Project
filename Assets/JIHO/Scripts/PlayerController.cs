using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State<PlayerController> currentState;

    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Vector3 playerVec;
    public GameObject attackCol;
    public CameraMove cameraMove;

    [Header("스테이터스")]
    public float jumpPower;
    public float walkSpeed;
    public float testDistance;
    public float rightDistance;
    public LayerMask testLayer;

    public float currentSpeed;
    public float attackDelay;
    public float currentDelay;

    public float currentHp;
    public float maxHp;

    public bool isJump;
    public bool isGround;
    public bool rightIsWall;
    public bool leftIsWall;

    private void Update()
    {
        PlayerInput();
        AttackDelay();
        RayCast();
    }

    private void RayCast()
    {
        if(Physics2D.Raycast(transform.position, Vector2.down, testDistance, testLayer))
        {
            Debug.Log("Ray");
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if (Physics2D.Raycast(transform.position, Vector2.right, rightDistance, testLayer))
        {
            Debug.Log("RightWall");
            rightIsWall = true;
            leftIsWall = false;
        }
        else
        {
            rightIsWall = false;
        }

        if (Physics2D.Raycast(transform.position, Vector2.left, rightDistance, testLayer))
        {
            Debug.Log("LeftWall");
            leftIsWall = true;
            rightIsWall = false;
        }
        else
        {
            leftIsWall = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void AttackDelay()
    {
        if (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
        }
    }
    private void PlayerInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump")) anim.SetTrigger("isJump");
            }
            else anim.SetBool("isMove", true);

            currentSpeed = walkSpeed;
        }
        else
        {
            currentSpeed = 0;
            anim.SetBool("isMove", false);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(isGround) Jump();
        }
    }


    public void Attack()
    {
        if (currentDelay > 0) return;
        anim.SetTrigger("Attack");
        currentDelay = attackDelay;

        Debug.Log("Attack!!");
    }

    public void AttackCol()
    {
        attackCol.SetActive(true);
    }

    public void Jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x < 0)
        {
            attackCol.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            spriteRenderer.flipX = true;
        }
        else if(x > 0)
        {
            attackCol.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            spriteRenderer.flipX = false;
        }
        if (x > 0 && rightIsWall) x = 0;
        else if (x < 0 && leftIsWall) x = 0;

        rigid.velocity = new Vector2(x * currentSpeed, rigid.velocity.y);

    }

    public void GetDamage()
    {
        currentHp -= 1;
    }

    public void StateChange(State<PlayerController> newState)
    {
        newState.StateChange(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CameraMove"))
        {
            
        }
    }
}
