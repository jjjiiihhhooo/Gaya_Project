using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            collision.gameObject.GetComponent<GetDamage>().OnHit(); //플레이어 대미지
            StartCoroutine("ICD"); // 내부쿨타임 시작
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어를 때렸다.");
            collision.gameObject.GetComponent<GetDamage>().OnHit(); //플레이어 대미지
            StartCoroutine("ICD"); // 내부쿨타임 시작
        }
    }
    IEnumerator ICD()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(AtcCoolTime);
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
}
