using System.Collections;
using TMPro;
using UnityEngine;

public class Boss : Enermy
{
    public Animator boss_anim;
    public SpriteRenderer spr;
    public GameObject[] effects;
    public Transform leftTransform;
    public Transform rightTransform;
    public Transform currentTarget;
    public CameraMove cameraMove;
    public GameObject bossWall_1;
    public GameObject bossWall_2;
    public TextMeshProUGUI text;

    public bool isLeft;
    public bool isAttack_1;
    public bool isAttack_2;
    public bool isCheck;
    public bool bossStart;

    public float x;


    private void Awake()
    {
        x = 0;
        currentHp = maxHp;
        bossStart = false;
    }

    private void Update()
    {
        if (!bossStart) return;
        if (isDie) BossDead();
        if (currentHp < 22) Attack_2_Ready();
        else Attack_1_Ready();

        Attack_Delay();
        if (isAttack_1) BossMove();
    }

    public void BossStart()
    {
        StartCoroutine(BossCor());
    }

    private IEnumerator BossCor()
    {
        
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(true);
        cameraMove.isMiddlePos = true;
        bossWall_1.SetActive(true);
        bossWall_2.SetActive(true);
        bossStart = true;
    }

    public void BossDead()
    {
        StartCoroutine(BossDeadCor());
    }

    private IEnumerator BossDeadCor()
    {
        yield return new WaitForSeconds(0.5f);
        text.gameObject.SetActive(false);
        cameraMove.isMiddlePos = false;
        bossWall_2.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Attack_1_Ready()
    {
        if(currentHp > 13)
        {
            if (currentDelay <= 0)
            {
                currentDelay = moveDelay;
                boss_anim.SetTrigger("a");
                boss_anim.SetBool("Attack1", true);
            }
        }
        else
        {
            boss_anim.SetTrigger("a");
            boss_anim.SetBool("Attack1", true);
        }
        

    }

    public void Attack_1()
    {
        if(!isLeft)
        {
            leftTransform.gameObject.SetActive(true);
            rightTransform.gameObject.SetActive(true);
            currentTarget = leftTransform;
            x = -1;
            isAttack_1 = true;
        }
        else
        {
            leftTransform.gameObject.SetActive(true);
            rightTransform.gameObject.SetActive(true);
            currentTarget = rightTransform;
            x = 1;
            isAttack_1 = true;
        }

    }

    public void BossGetDamage(float damage)
    {
        currentHp -= damage;
        
        if (currentHp <= 0) isDie = true;

        
    }

    public void Attack_Delay()
    {
        if (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
            isCheck = false;
        }
    }

    public void BossMove()
    {
        if (isCheck)
        {
            isAttack_1 = false;
            leftTransform.gameObject.SetActive(false);
            rightTransform.gameObject.SetActive(false);
            return;
        }
        //if (currentTarget == leftTransform) transform.position += Vector3.left * 6 * Time.deltaTime;
        //else transform.position += Vector3.right * 6 * Time.deltaTime;

        rb.velocity = new Vector2(x * 6, rb.velocity.y);
    }

    public void Attack_2()
    {
        Debug.Log("����");
        for(int i = 0; i < effects.Length; i++)
        {
            effects[i].SetActive(true);
        }
        Attack_1_Ready();
    }

    public void Attack_2_Ready()
    {
        if (currentDelay <= 0)
        {
            isAttack_1 = false;
            currentDelay = moveDelay;
            StartCoroutine(Attack_2Cor());
        }
    }

    private IEnumerator Attack_2Cor()
    {
        boss_anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(2f);
        Attack_2();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BossCol"))
        {
            if(collision.transform == currentTarget)
            {
                spr.flipX = !spr.flipX;
                isLeft = !isLeft;
                isCheck = true;
                boss_anim.SetBool("Attack1",false);
            }
        }
    }
}
