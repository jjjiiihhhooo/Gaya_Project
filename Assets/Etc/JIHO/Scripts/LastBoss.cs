using System.Collections;
using UnityEngine;

public class LastBoss : MonoBehaviour
{
    

    
    public Vector3 playerVec;

    public GameObject fireball;
    public float currentHp;
    public float maxHp;

    public float marbleCount;

    public marble[] marbles;

    public bool isAttack;

    public Animator anim;

    public Rotate rotate;

    public GameObject wall_1;
    public GameObject wall_2;

    public GameObject whiteFade;
    public bool isStop;
    public bool isBoss;
    public bool isDead;

    private void Awake()
    {
        
            currentHp = maxHp;
            StartCoroutine(StartCor());
        
    }

    public void BossStart()
    {
        isBoss = true;
        Sound.instance.Play(Sound.instance.audioDictionary["last"], true);
    }

    private IEnumerator StartCor()
    {
        yield return new WaitForSeconds(2f);
        BossStart();
    }

    private void Update()
    {
        if (isDead) return;

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("bonghwang_idle") && isBoss)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
                Dash();
            else
                AttackPosition();
        }
    }

    public void Dash()
    {
        isAttack = true;
        anim.SetTrigger("dash");
    }

    public void MarbleCount()
    {
        marbleCount++;
        if(marbleCount >= 4)
        {
             StopMove();
            marbleCount = 0;
        }
    }

    public void AttackPosition()
    {
        anim.SetBool("isMarble",true);
        for(int i = 0; i < marbles.Length; i++)
        {
            marbles[i].gameObject.SetActive(true);
        }
    }

    public void GetDamage()
    {
        currentHp--;
        if (currentHp <= 0) Die();
    }

    public void Die()
    {
        StartCoroutine(DieCor());    
    }

    private IEnumerator DieCor()
    {
        anim.SetTrigger("Die");
        isDead = true;
        yield return new WaitForSeconds(1.5f);

    }

    public void Fade()
    {
        StartCoroutine(FadeCor());
    }

    private IEnumerator FadeCor()
    {
        whiteFade.SetActive(true);
        yield return new WaitForSeconds(1f);
        whiteFade.SetActive(false);
    }

    public void StopMove()
    {
        isStop = true;
        anim.SetBool("isMarble", false);
        anim.SetTrigger("StopMove");
        isAttack = true;
        
    }



}
