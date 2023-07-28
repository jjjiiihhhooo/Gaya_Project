using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBoss : Enemy
{
    [Header("보스 상태표시")]
    public bool OnPatton_01;
    public bool OnPatton_02;
    public bool OnStun;
    public bool OnPatton_01_Move;

    [Header("Patton01")]
    [SerializeField] private GameObject Boss_Spawn_Pos;
    [SerializeField] private GameObject Patton01_Appear_Pos;
    [SerializeField] private float Patton01_Speed = 1;
    [SerializeField] private float Patton01_Time = 1;

    [Header("Patton02")]
    public float StunTime = 1;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OnPatton_01 = false;
        OnPatton_02 = false;
        OnStun = false;
        OnPatton_01_Move = false;
        animator.Play("Final_Boss_Idle");
        StartCoroutine("Patton01");
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnPatton_01 && !OnPatton_02 && !OnStun) // 패턴중이 아니면
        {
            //패턴을 시작한다.
            //Patton_02();
            //StartCoroutine("Patton01");
        }

        if (OnPatton_02) // 패턴 2 중일때 패턴이 끝났는가 확인
        {
            OnPatton_02 = !this.gameObject.transform.GetChild(1).gameObject.GetComponent<FinalBoss_Patton_02>().isEndPatton_02();// 패턴이 끝났는가 확인
            if (!OnPatton_02) // 패턴이 끝났으면
            {
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(Stun());
            }
        }

        if (OnPatton_01_Move)
        {
            Patton01_Move();
        }
    }

    public override void Die()
    {
        base.Die();
    }

    #region Patton1
    IEnumerator Patton01()
    {
        OnPatton_01 = true;
        animator.Play("Final_Boss_Disappear");
        yield return new WaitForSeconds(0.7f); // 애니메이션
        this.gameObject.transform.position = Patton01_Appear_Pos.transform.position; // 맵 옆으로 이동
        // 전조증상 표시필요
        yield return new WaitForSeconds(1); // 애니메이션
        OnPatton_01_Move = true; // 움직임
        animator.Play("Final_Boss_Patton_01");
        yield return new WaitForSeconds(Patton01_Time); // 움직이는 시간
        OnPatton_01_Move = false; // 다시정리
        this.gameObject.transform.position = Boss_Spawn_Pos.transform.position;
        animator.Play("Final_Boss_Appear");
        yield return new WaitForSeconds(0.7f); // 애니메이션
        // 다시 나타내야함.위에서 나타나던가 사라지는거 다시 돌려서 나타내던가
        animator.Play("Final_Boss_Idle");
        OnPatton_01 = false;
    }

    private void Patton01_Move()
    {
        this.gameObject.transform.position += new Vector3(-Patton01_Speed,0,0) * Time.deltaTime;
    }

    #endregion

    #region Patton2
    public override void Stun(bool _Stun)
    {
        base.Stun(_Stun);
    }

    private void Patton_02()
    {
        OnPatton_02 = true;
        animator.Play("Final_Boss_Patton_02");
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true); // 2번패턴 시작
        SetBeria(true);
    }
    private void SetBeria(bool _Beria)
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(_Beria); // 베리어가 켜지면 콜라이더 꺼지고
        this.gameObject.GetComponent<Collider2D>().enabled = !_Beria; // 베리어가 꺼지면 콜라이더가 켜진다.
    }

    public IEnumerator Stun()
    {
        OnStun = true;
        SetBeria(false);
        animator.Play("Final_Boss_Stun_Ready");
        yield return new WaitForSeconds(0.5f);
        animator.Play("Final_Boss_Stun");
        yield return new WaitForSeconds(StunTime); //스턴시간
        OnStun = false;
        animator.Play("Final_Boss_Idle");
    }
    #endregion


}
