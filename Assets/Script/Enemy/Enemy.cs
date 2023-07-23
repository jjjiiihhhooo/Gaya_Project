using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // 현재 체력
    public float maxHp; // 최대 체력
    public Animator animator; // 애니메이션 컨트롤러
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //맞는소리

    public virtual void Start()
    {
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
            Die();
        }
    }

    IEnumerator GetDamaged()
    {
        Effect.SetActive(true); // 이펙트 표시
        animator.SetBool("isDamage", true);
        Stun(true);
        yield return new WaitForSeconds(0.1f);
        Stun(false);
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // 이펙트 표시
    }

    public virtual void Die()
    {

    }

    public virtual void Stun(bool _Stun)
    {

    }
}
