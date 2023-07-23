using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // ���� ü��
    public float maxHp; // �ִ� ü��
    public Animator animator; // �ִϸ��̼� ��Ʈ�ѷ�
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //�´¼Ҹ�

    public virtual void Start()
    {
        Effect = this.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public virtual void GetDamage(float damage)
    {
        Debug.Log(this.gameObject.name + " : �¾Ҵ�!");
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
        Effect.SetActive(true); // ����Ʈ ǥ��
        animator.SetBool("isDamage", true);
        Stun(true);
        yield return new WaitForSeconds(0.1f);
        Stun(false);
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // ����Ʈ ǥ��
    }

    public virtual void Die()
    {

    }

    public virtual void Stun(bool _Stun)
    {

    }
}
