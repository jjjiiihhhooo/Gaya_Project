using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;
    public Enemy_Move move;

    public float currentHp; // ���� ü��
    public float maxHp; // �ִ� ü��
    private Animator animator; // �ִϸ��̼� ��Ʈ�ѷ�
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //�´¼Ҹ�

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
        Debug.Log(this.gameObject.name + " : �¾Ҵ�!");
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
        Effect.SetActive(true); // ����Ʈ ǥ��
        animator.SetBool("isDamage", true);
        move.enabled = false;
        yield return new WaitForSeconds(0.4f);
        move.enabled = true;
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // ����Ʈ ǥ��
    }
}
