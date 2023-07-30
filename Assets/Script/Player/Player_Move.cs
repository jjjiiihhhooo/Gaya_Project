using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player_Move : MonoBehaviour
{
    // ������ �����ϴ� ��
    [Header("-------------------------������ �����ϴ� ��------------------------- ")]
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float moveSpeed = 6;

    float gravity;
    float jumpVelocity;

    [Header("-------------------------��� ��------------------------- ")]
    public Vector3 velocity;
    public Vector2 input;
    
    // ����
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
        if(controller.collisions.above || controller.collisions.below) // ���̳� õ�忡 ������
        {
            velocity.y = 0;
        }

        if (!isStun)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // �Է°� �ޱ�
            FlipX();
        }

        if (Input.GetKeyDown(KeyCode.X) && controller.collisions.below && !isStun) // ���� ���� ����� ��� �Է��� �ް�
        {
            animator.SetBool("isJump", true);
            velocity.y = jumpVelocity;
        }
        else
        {
            animator.SetBool("isJump", false);
        }

        float targetVelocityX = input.x * moveSpeed; // �¿� ������ �ӵ� 
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
        }
    }
}
