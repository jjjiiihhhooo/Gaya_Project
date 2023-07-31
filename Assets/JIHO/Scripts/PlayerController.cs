
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    

    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Vector3 playerVec;
    public GameObject attackCol;
    public CameraMove cameraMove;
    public Sprite[] hearts;
    public Image[] curHeart;
    public GameObject effect;
    public TextMeshProUGUI lifeCountText;
    public GameObject out_obj;

    public LastBoss last;
    public Vector3 startPos;

    public Boss middleBoss;

    [Header("스테이터스")]
    public float jumpPower;
    public float walkSpeed;
    public float testDistance;
    public float rightDistance;
    public LayerMask testLayer;

    public float currentSpeed;
    public float attackDelay;
    public float currentDelay;

    public int LifeCount;

    public float hitDelay;
    public float currentHitDelay;

    public float currentHp;
    public float maxHp;
    public float num;

    public bool isJump;
    public bool isGround;
    public bool rightIsWall;
    public bool leftIsWall;
    public bool isHit;

    private void Awake()
    {
        cameraMove = FindObjectOfType<CameraMove>();
        currentHp = maxHp;
        startPos = transform.position;
    }

    private void Update()
    {
        PlayerInput();
        AttackDelay();
        HitDelay();
        RayCast();
        if(last != null) last.playerVec = transform.position;
    }

    private void HitDelay()
    {
        if (currentHitDelay >= 0)
        {
            currentHitDelay -= Time.deltaTime;
        }
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

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(isGround) Jump();
        }
    }


    public void Attack()
    {
        if (currentDelay > 0) return;
        if (isHit) return;
        anim.SetTrigger("Attack");
        currentDelay = attackDelay;

        Debug.Log("Attack!!");
    }

    public void AttackCol()
    {
        Sound.instance.Play(Sound.instance.audioDictionary["NoHit"], false);
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

    public void GetDamage(Transform target)
    {
        currentHitDelay = hitDelay;
        currentHp -= 1;
        if (currentHp == 5)
        {
            curHeart[1].sprite = hearts[0];
            curHeart[2].sprite = hearts[0];
            curHeart[0].sprite = hearts[1];
        }
        else if(currentHp == 4)
        {
            curHeart[1].sprite = hearts[0];
            curHeart[2].sprite = hearts[0];
            curHeart[0].sprite = hearts[2];
        }
        else if (currentHp == 3)
        {
            curHeart[1].sprite = hearts[1];
            curHeart[2].sprite = hearts[0];
            curHeart[0].sprite = hearts[2];
        }
        else if (currentHp == 2)
        {
            curHeart[1].sprite = hearts[2];
            curHeart[2].sprite = hearts[0];
            curHeart[0].sprite = hearts[2];
        }
        else if (currentHp == 1)
        {
            curHeart[1].sprite = hearts[2];
            curHeart[2].sprite = hearts[1];
            curHeart[0].sprite = hearts[2];
        }
        else if (currentHp == 0)
        {
            curHeart[1].sprite = hearts[2];
            curHeart[2].sprite = hearts[2];
            curHeart[0].sprite = hearts[2];
            //Die();
        }

        if (transform.position.x - target.position.x >= 0) transform.position = new Vector2(transform.position.x + num, transform.position.y);
        else
        {
            transform.position = new Vector2(transform.position.x - num, transform.position.y);
        }
        anim.SetTrigger("Hit");
    }

    public void Die()
    {
        StartCoroutine(DieCor());
    }

    private IEnumerator DieCor()
    {
        Respawn();
        LifeCount--;
        out_obj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        lifeCountText.text = "L";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Li";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Lif";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life : ";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life : " + LifeCount.ToString();
        yield return new WaitForSeconds(0.7f);
        out_obj.SetActive(false);
    }

    public void Respawn()
    {
        currentHp = maxHp;
        transform.position = startPos;
        cameraMove.transform.position = startPos;
        curHeart[0].sprite = hearts[0];
        curHeart[1].sprite = hearts[0];
        curHeart[2].sprite = hearts[0];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (currentHitDelay <= 0) GetDamage(collision.transform);
        }

        if (collision.transform.CompareTag("Boss"))
        {
            if (currentHitDelay <= 0)
            {
                GetDamage(collision.transform);
                if (currentHp <= 0)
                {
                    collision.transform.GetComponent<Boss>().Init();
                    Die();
                    Sound.instance.Play(Sound.instance.audioDictionary["Start"], true);
                }
            }
        }

        if (collision.transform.CompareTag("lastBoss"))
        {
            if (currentHitDelay <= 0)
            {
                GetDamage(collision.transform);
                if (currentHp <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
            if (collision.transform.CompareTag("fireball"))
        {
            if (currentHitDelay <= 0)
            {
                if (collision != null)
                {
                    GetDamage(collision.transform);
                    if (currentHp <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
        }  }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossStart"))
        {
            if(middleBoss.gameObject.activeSelf) middleBoss.BossStart();

        }
        if (collision.CompareTag("lastBossStart"))
        {
            last.BossStart();
        }

        if (collision.CompareTag("Out"))
        {
            Die();
        }

        if(collision.CompareTag("Start"))
        {
            cameraMove.isStart = true;
        }
    }
}
