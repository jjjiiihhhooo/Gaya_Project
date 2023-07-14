using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State<PlayerController> currentState;

    public PlayerIdleState playerIdleState;
    public PlayerWalkState playerWalkState;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Vector3 playerVec;
    public GameObject attackCol;

    [Header("스테이터스")]
    public float jumpPower;
    public float walkSpeed;
    public float testDistance;
    public LayerMask testLayer;

    public float currentSpeed;
    public float attackDelay;
    public float currentDelay;

    public bool isJump;
    public bool isGround;


    private void Awake()
    {
        Init();
    }

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

    private void Init()
    {
        playerIdleState = new PlayerIdleState();
        playerWalkState = new PlayerWalkState();

        currentState = playerIdleState;
    }

    private void PlayerInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Vertical") != 0) anim.SetTrigger("isJump");
            else anim.SetBool("isMove", true);

            if (currentState.GetType() != typeof(PlayerWalkState)) StateChange(playerWalkState);
        }
        else
        {
            anim.SetBool("isMove", false);
            if (currentState.GetType() != typeof(PlayerIdleState)) StateChange(playerIdleState);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public void Idle()
    {

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

    public void Move()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x < 0)
        {
            attackCol.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
            spriteRenderer.flipX = true;
        }
        else
        {
            attackCol.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            spriteRenderer.flipX = false;
        }

        playerVec.x = x;
        if(isGround) playerVec.y = y * jumpPower;

        transform.position += playerVec * currentSpeed * Time.fixedDeltaTime;
    }

    public void GetDamage()
    {

    }

    public void StateChange(State<PlayerController> newState)
    {
        newState.StateChange(this);
    }
}
