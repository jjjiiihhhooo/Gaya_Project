using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Enemy_Move_Bird : MonoBehaviour
{
    Vector2 nowPosition;
    [Header("플레이어 탐지 및 탐지 길이")]
    [SerializeField] private LayerMask PlayerlayerMask;
    [SerializeField] private float DetectPlayerDistance = 0.5f;

    [Header("새의 상태")]
    [SerializeField] bool inAttack;

    [Header("내려가고 올라가는 속도 및 떨어지는 시간")]
    [SerializeField] float attackSpeed;
    [SerializeField] float attackTime;

    // 컴포넌트
    private Animator animator;
    private SpriteRenderer sprite;

    //이동변수
    [Header("양옆으로의 이동범위")]
    [SerializeField] float sidewith;
    [SerializeField] float MoveSpeed;

    //변수
    private bool OnUp;
    private Vector3 startPos;
    bool isRight;

    private void Start()
    {
        isRight = false;
        startPos = transform.position;
        OnUp = false;
        inAttack = false;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SetPos(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        DetectPlayer();

        if (inAttack) // 공격중
        {
            Move_Attack(OnUp);
        }
        else // 평상시
        {
            Move();
        }
    }

    public void Move()
    {
        if(this.gameObject.transform.position.x > startPos.x + sidewith) // 시작위치 + 오차범위 보다 오른쪽이면
        {
            isRight = false;
        }
        if(this.gameObject.transform.position.x < startPos.x - sidewith) // 시작위치 - 오차범위 보다 왼쪽이면
        {
            isRight = true;
        }

        if(isRight)
        {
            this.gameObject.transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime; // 오른쪽이동
            sprite.flipX = true;
        }
        else
        {
            this.gameObject.transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime; // 왼쪽이동
            sprite.flipX = false;
        }
    }

    public void Move_Attack(bool onUp)
    {
        if(onUp)
        {
            this.gameObject.transform.position += new Vector3(0, attackSpeed, 0) * Time.deltaTime; // 상승
        }
        else
        {
            this.gameObject.transform.position += new Vector3(0, -attackSpeed, 0) * Time.deltaTime; // 하강
        }
    }

    public void DetectPlayer()
    {
        nowPosition = SetPos(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        RaycastHit2D GroundHit = Physics2D.Raycast(nowPosition, Vector2.down, DetectPlayerDistance, PlayerlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.down * DetectPlayerDistance, Color.blue);

        if (GroundHit.collider != null && !inAttack )
        {
            Debug.Log("플레이어를 발견했다!");
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        inAttack = true; // 공격을 한다
        animator.SetBool("isAttack",true); // 공격모션
        OnUp = false;
        print("하강!");
        yield return new WaitForSeconds(attackTime); // 하강
        OnUp = true;
        animator.SetBool("isAttack", false); // 종료
        yield return new WaitForSeconds(attackTime); // 상승
        inAttack = false;
    }
    
    public Vector2 SetPos(float pos_x, float pos_y)
    {
        return new Vector2(pos_x, pos_y);
    }
}
