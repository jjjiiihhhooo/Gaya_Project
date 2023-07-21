using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // 현재 체력
    public float maxHp; // 최대 체력
    private Animator animator; // 애니메이션 컨트롤러
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //맞는소리

    protected virtual void Start()
    {
        Effect = this.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public void GetDamage(float damage)
    {
        Debug.Log(this.gameObject.name + " : 맞았다!");
        Sound.instance.Play(Hit_sound, false);
        StartCoroutine("GetDamaged");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator GetDamaged()
    {
        Effect.SetActive(true); // 이펙트 표시
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // 이펙트 표시
    }
}
