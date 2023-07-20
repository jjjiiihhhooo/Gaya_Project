using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Attack : MonoBehaviour
{
    [Header("공격 딜레이")]
    public float attackDelay = 1.0f; // 공격 딜레이

    [Header("사운드 소스")]
    public AudioClip attackSound = null;
    //컴포넌트
    Animator animator;

    //변수
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

        if (Input.GetKeyDown(KeyCode.Z) && currentDelay == 0) // 공격버튼
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        Sound.instance.Play(attackSound, false); // 사운드
        currentDelay = attackDelay; // 쿨타임

        attackArea.SetActive(true); 
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f);

        attackArea.SetActive(false);
        animator.SetBool("isAttack", false);

    }
}