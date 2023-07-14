using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public int ASDJKASD;
    public Transform target;
    public float curSpeed, isDescent, distance, fy, ys, fx;
    public bool isAttack;
    public int direction;
    public bool isDie;
    public float currentHp;
    public float maxHp;
    public Animator eAnimator;

    public float moveDelay;
    public float currentDelay;
    public float times;

    public float Pdistance;
    public bool isMove;
    public bool isDamage;
    public float num;
    float dir = 0;
    void Start()
    {
        currentHp = maxHp;
        direction = 1;
        isDescent = -1;
        curSpeed = 3f;

        fy = transform.position.y;
        fx = transform.position.x;
        isMove = false;
        times = 2;
    }

    public void StartMove()
    {
        isMove = true;
    }

    public void StopMove()
    {
        isMove = false;
    }

    public void GetDamage(float damage)
    {
        if (transform.position.x - target.position.x >= 0) transform.position = new Vector2(transform.position.x + num, transform.position.y);
        else
        {
            transform.position = new Vector2(transform.position.x - num, transform.position.y);
        }
        currentHp -= damage;
        if (currentHp <= 0) isDie = true;
    }

    void Update()
    {
        if (isDie)
        {
            Destroy(gameObject);
        }

        if (!isAttack)
        {
            Move();
            if (distance * direction > -2 && distance * direction < -1.2)
            {
                isAttack = true;
                isDescent = -1;
            }
            if (distance * direction > -0.2f)
            {
                isAttack = true;
                direction = 0;
                isDescent = -1;
            }
        }
        if (isAttack)
        {
            Attack();
        }
    }

    void Move()
    {
        distance = transform.position.x - target.position.x;
        if (distance < 0)
        {
            direction = 1;
        }
        else if (distance > 0)
        {
            direction = -1;
        }

        if (fx - transform.position.x < ASDJKASD)
        {
            if(direction == -1)
            {
                if (distance > -10 && distance < 10)
                {
                    transform.Translate(new Vector2(direction, 0) * Time.deltaTime * curSpeed);
                }
            }
        }
        if (fx - transform.position.x > -ASDJKASD)
        {
            if (direction == 1)
            { 
                if (distance > -10 && distance < 10)
                {
                    transform.Translate(new Vector2(direction, 0) * Time.deltaTime * curSpeed);
                }
            }
        }

    }

    void Attack()
    {
        //ys = (1.2f - (fy - transform.position.y)) / 1.4f;
        //transform.Translate(new Vector2(direction, isDescent * ys) * Time.deltaTime * curSpeed);
        //if(direction != 0)
        //    transform.localScale = new Vector3(direction, 1, 1);
        transform.Translate(new Vector2(direction, isDescent) * Time.deltaTime * curSpeed * times);
        Debug.Log("Attack");
        if (target.position.y >= transform.position.y)
        {
            isDescent = 1;
            direction = -direction;
            times = 1.5f;
        }
        if (fy < transform.position.y)
        {
            isAttack = false;
            times = 2;
        }
    }
}
