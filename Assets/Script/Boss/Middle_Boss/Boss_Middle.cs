using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Animator))]
public class Boss_Middle : Enemy
{
    /// <summary>
    /// 보스의 움직임과 패턴 죽음을 관리하는 클래스
    /// </summary>
    
    [SerializeField] private StartBossStage startBossStage; // 보스 시작알리미

    //컴포넌트
    private Animator animator; // 애니메이션


    //변수
    [Header("돌진패턴 변수 스피드, 이동시간")]
    [SerializeField] private float MoveSpeed = 1;
    [SerializeField] private float MoveRange = 1;
    private bool isMove = false; // 움직임

    private void OnEnable()
    {
        isMove = false;
        animator = GetComponent<Animator>(); // 컴포넌트 가져오기
        currentHp = maxHp; //체력 재정비
    }

    public override void Start()
    {
        base.Start();
        //StartCoroutine("Patton_01");

    }

    private void Update()
    {
        if(isMove)
        {
            Patton_01_Move();
        }
    }

    #region Patton_01
    IEnumerator Patton_01()
    {
        animator.SetInteger("Patton01", 1);
        yield return new WaitForSeconds(1);
        animator.SetInteger("Patton01", 2);
        isMove = true; //이동
        yield return new WaitForSeconds(MoveRange);
        isMove = false; // 이동중지
        this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX;
        animator.SetInteger("Patton01", 0);
        yield return new WaitForSeconds(1); // 대기시간
    }

    private void Patton_01_Move()
    {
        if (!this.gameObject.GetComponent<SpriteRenderer>().flipX) // 왼쪽이동 오른쪽이동
        {
            this.gameObject.transform.position += Vector3.left * Time.deltaTime * MoveSpeed;
        }
        else
        {
            this.gameObject.transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
        }
    }
    #endregion
    #region Patton_02
    IEnumerator Patton_02()
    {
        yield return new WaitForSeconds(1);
    }
    #endregion
    public override void Die()
    {
        startBossStage.RemoveWalls(); // 스테이지 클리어
    }
}
