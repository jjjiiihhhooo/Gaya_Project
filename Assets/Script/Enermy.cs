using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Enermy : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    protected float curSpeed, attackDelay, spawnX;
    protected int enermyVision, adiya;
    protected bool isDie = false, goBack = false;
    public Animator eAnimator;
    public float test;
    


    protected virtual void Start()
    {
        curSpeed = 1.1f;
        attackDelay = 0f;
        rb = GetComponent<Rigidbody2D>();
        spawnX = 7;
        
    }
    private void Update()
    {
        if (isDie)
        {
            Destroy(gameObject);
        }
        float distance = Vector3.Distance(rb.position, target.position);
        test = spawnX - transform.position.x;
        if (attackDelay <= 0f) 
        {
            if (spawnX - transform.position.x < -10 || spawnX - transform.position.x > 10 || goBack)
            {
                goBack = true;
                GoBack();
            }
            if (!goBack)
            {
                if (distance < 5)
                {
                    if (distance < 0.4f)
                    {
                        eAnimator.SetTrigger("Attack");
                        Debug.Log("Attack");
                        attackDelay = 0.5f;
                    }
                    else
                    {
                        Move();
                    }
                }
            }
        }
        else
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0f)
            {
                attackDelay = 0f;
            }
        }
            
    }
    protected virtual void Move()
    {
        float dir = target.position.x - transform.position.x;
        int LR;
        if (dir < 0)
        {
            LR = -1;
        }
        else
        {
            LR = 1;
        }
        if (!isDie)
        {
            transform.Translate(new Vector2(LR, 0) * curSpeed * Time.deltaTime);
            if (dir < 0) //플레이어가 왼쪽에 있을때
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else  //플레이어가 오른쪽에 있을때
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        eAnimator.SetTrigger("Walk");
        Debug.Log("Walk");
    }
    protected virtual void GoBack()
    {
        float dir = spawnX - transform.position.x;
        if (dir < 0.5 && dir > -0.5)
        {
            goBack = false;
        }
        int LR;
        if (dir < 0)
        {
            LR = -1;
        }
        else
        {
            LR = 1;
        }
        if (!isDie)
        {
            transform.Translate(new Vector2(LR, 0) * curSpeed * Time.deltaTime * 3);
            if (dir < 0) //플레이어가 왼쪽에 있을때
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else  //플레이어가 오른쪽에 있을때
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }
}
