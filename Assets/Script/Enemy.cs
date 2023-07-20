using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // 현재 체력
    public float maxHp; // 최대 체력
    private Animator animator; // 애니메이션 컨트롤러


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Enemy : 맞았다!");
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
