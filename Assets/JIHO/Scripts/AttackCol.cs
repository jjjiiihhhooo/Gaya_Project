using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    public float time;
    public float damage;

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time < 0.1f) time += Time.deltaTime;
        else this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enermy>().GetDamage(damage);
        }
    }
}
