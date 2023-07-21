using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected float curSpeed, attackDelay;

    public float currentHp; // ���� ü��
    public float maxHp; // �ִ� ü��
    private Animator animator; // �ִϸ��̼� ��Ʈ�ѷ�
    private GameObject Effect;
    [SerializeField] AudioClip Hit_sound; //�´¼Ҹ�

    protected virtual void Start()
    {
        Effect = this.transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        attackDelay = 0f;
    }

    public void GetDamage(float damage)
    {
        Debug.Log(this.gameObject.name + " : �¾Ҵ�!");
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
        Effect.SetActive(true); // ����Ʈ ǥ��
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDamage", false);
        Effect.SetActive(false); // ����Ʈ ǥ��
    }
}
