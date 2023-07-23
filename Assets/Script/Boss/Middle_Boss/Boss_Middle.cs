using System.Collections;
using UnityEngine;

public class Boss_Middle : Enemy
{
    /// <summary>
    /// 보스의 움직임과 패턴 죽음을 관리하는 클래스
    /// </summary>
    
    [SerializeField] private StartBossStage startBossStage; // 보스 시작알리미
    [SerializeField] private GameObject Patton02_Arrow;

    //변수
    [Header("돌진패턴 변수 스피드, 이동시간")]
    [SerializeField] private float MoveSpeed = 1;
    [SerializeField] private float MoveRange = 1;
    
    private bool isMove = false; // 움직임
    private bool onPatton = false;
    private bool isClear = false;

    private void OnEnable()
    {
        onPatton = false;
        isMove = false;
        animator = GetComponent<Animator>(); // 컴포넌트 가져오기
        currentHp = maxHp; //체력 재정비
    }

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(isMove)
        {
            Patton_01_Move();
        }

        if (!onPatton)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                StartCoroutine("Patton_01");
            }
            else
            {
                StartCoroutine("Patton_02");
            }
        }
    }


    #region Patton_01
    IEnumerator Patton_01()
    {
        onPatton = true;
        animator.SetInteger("Patton01", 1);
        yield return new WaitForSeconds(1);
        animator.SetInteger("Patton01", 2);
        isMove = true; //이동
        yield return new WaitForSeconds(MoveRange);
        isMove = false; // 이동중지
        this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX;
        animator.SetInteger("Patton01", 0);
        yield return new WaitForSeconds(1); // 대기시간
        onPatton = false;
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
        onPatton = true; // 패턴시작

        animator.SetBool("Patton02", true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Patton02", false);
        Patton02_Arrow.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        Patton02_Arrow.SetActive(false);

        onPatton = false; // 패턴끝
    }

    #endregion

    public override void Die()
    {
        StopCoroutine("StartMiddleBoss");
        StartCoroutine("DieAnimation");
        startBossStage.RemoveWalls(); // 스테이지 클리어
    }

    IEnumerator DieAnimation()
    {
        animator.Play("Boss_Middle_Die");
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }
}
