using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // ���� ü��
    public float maxHp; // �ִ� ü��
    private Animator animator; // �ִϸ��̼� ��Ʈ�ѷ�


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Enemy : �¾Ҵ�!");
        StartCoroutine("GetDamaged");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    IEnumerable GetDamaged()
    {
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDamage", false);
    }
}
