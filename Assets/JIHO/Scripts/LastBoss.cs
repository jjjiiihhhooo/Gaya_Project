using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : MonoBehaviour
{
    public static LastBoss instance;

    
    public Vector3 playerVec;

    public GameObject fireball;
    public float currentHp;
    public float maxHp;

    public float marbleCount;

    public marble[] marbles;

    public bool isAttack;

    public Animator anim;

    public Rotate rotate;

    public bool isStop;
    public bool isBoss;
    public bool isDead;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            currentHp = maxHp;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void BossStart()
    {
        isBoss = true;
        Sound.instance.Play(Sound.instance.audioDictionary["last"], true);
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

    public void StopMove()
    {
        isStop = true;
        anim.SetBool("isMarble", false);
        anim.SetTrigger("StopMove");
        isAttack = true;
        
    }



}
