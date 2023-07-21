using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [Header("공격 쿨타임")]
    public float AtcCoolTime = 3;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("플레이어를 때렸다.");
            collision.gameObject.GetComponent<GetDamage>().OnHit();
            StartCoroutine("ICD");
        }
    }

    IEnumerator ICD()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(AtcCoolTime);
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
}
