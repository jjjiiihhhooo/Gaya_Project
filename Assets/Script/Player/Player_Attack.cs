using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Attack : MonoBehaviour
{
    [Header("���� ������")]
    public float attackDelay = 1.0f; // ���� ������

    [Header("���� �ҽ�")]
    public AudioClip attackSound = null;
    //������Ʈ
    Animator animator;

    //����
    private GameObject attackArea;
    private float currentDelay = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackArea = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        currentDelay -= Time.deltaTime;
        currentDelay = Mathf.Clamp(currentDelay, 0, attackDelay);

        if (Input.GetKeyDown(KeyCode.Z) && currentDelay == 0) // ���ݹ�ư
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        Sound.instance.Play(attackSound, false); // ����
        currentDelay = attackDelay; // ��Ÿ��

        attackArea.SetActive(true); 
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f);

        attackArea.SetActive(false);
        animator.SetBool("isAttack", false);

    }
}