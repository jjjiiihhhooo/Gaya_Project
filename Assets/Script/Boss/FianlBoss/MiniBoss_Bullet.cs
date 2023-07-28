using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss_Bullet : Enemy
{
    public Vector3 vector3 = new Vector3(0,0,0);
    private float time = 0;
    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    private void Update()
    {
        move(vector3);
        time += Time.deltaTime;
        
        if(time > 9)
        {
            Destroy(gameObject);
        }
    }

    public override void GetDamage(float damage)
    {
        Debug.Log(this.gameObject.name + " : ¸Â¾Ò´Ù!");
        Sound.instance.Play(Hit_sound, false);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }


    private void move(Vector3 vector)
    {
        gameObject.transform.position += vector * Time.deltaTime;
    }
}
