using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;
    public Enemy_Move move;

    public float currentHp; // 현재 체력
    public float maxHp; // 최대 체력
    private Animator animator; // 애니메이션 컨트롤러
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //맞는소리

    public virtual void Start()
    {
        move = GetComponent<Enemy_Move>();
        Effect = this.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public virtual void GetDamage(float damage)
    {
        Debug.Log(this.gameObject.name + " : 맞았다!");
        Sound.instance.Play(Hit_sound, false);
        StartCoroutine("GetDamaged");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
            Die();
        }
    }

    public virtual void Die()
    {

    }

    IEnumerator GetDamaged()
    {
        Effect.SetActive(true); // 이펙트 표시
        animator.SetBool("isDamage", true);
        move.enabled = false;
        yield return new WaitForSeconds(0.4f);
        move.enabled = true;
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // 이펙트 표시
    }
}
