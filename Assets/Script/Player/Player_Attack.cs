using System.Collections;
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
        float input = Input.GetAxisRaw("Horizontal"); // 입력값 받기
        AttackSide(input);
        currentDelay -= Time.deltaTime;
        currentDelay = Mathf.Clamp(currentDelay, 0, attackDelay);

        if (Input.GetKeyDown(KeyCode.Z) && currentDelay == 0) // 공격버튼
        {
            StartCoroutine("Attack");
        }
    }

    private void AttackSide(float _input)
    {
        if (_input == -1)
        {
            attackArea.gameObject.transform.localPosition = new Vector3(-1, 0, 0);
        }else if (_input == 1)
        {
            attackArea.gameObject.transform.localPosition = new Vector3(1, 0, 0);
        }
    }

    IEnumerator Attack()
    {
        Sound.instance.Play(attackSound, false); // 사운드
        currentDelay = attackDelay; // 쿨타임
        Debug.Log("때렷따!");
        attackArea.SetActive(true); 
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f);

        attackArea.SetActive(false);
        animator.SetBool("isAttack", false);
    }
}