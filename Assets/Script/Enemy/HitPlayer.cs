using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [Header("���� ��Ÿ��")]
    public float AtcCoolTime = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ���ȴ�.");
            collision.gameObject.GetComponent<GetDamage>().OnHit(); //�÷��̾� �����
            StartCoroutine("ICD"); // ������Ÿ�� ����
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ���ȴ�.");
            collision.gameObject.GetComponent<GetDamage>().OnHit(); //�÷��̾� �����
            StartCoroutine("ICD"); // ������Ÿ�� ����
        }
    }
    IEnumerator ICD()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(AtcCoolTime);
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
}
