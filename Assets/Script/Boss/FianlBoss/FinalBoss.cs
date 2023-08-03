using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBoss : Enemy
{
    [Header("보스 상태표시")]
    public bool OnPatton_02;
    public bool OnStun;
    public bool OnPatton_01_Move;
    public bool OnPatton_01;
    public AudioClip clearSound;

    [Header("Patton01")]
    [SerializeField] private GameObject Boss_Spawn_Pos;
    [SerializeField] private GameObject Patton01_Appear_Pos00;
    [SerializeField] private GameObject Patton01_Appear_Pos01;
    [SerializeField] private GameObject Patton01_Appear_Pos02;
    [SerializeField] private float Patton01_Speed = 1;
    [SerializeField] private float Patton01_Time = 1;
    [SerializeField] private GameObject Warning_Sign;
    [SerializeField] private float Patton01_Term = 5;

    [Header("Patton02")]
    public float StunTime = 1;

    [Header("Patton")]
    [SerializeField] private float term = 1;

    // 변수
    [Header("시간변수")]
    [SerializeField] private float time = 0;
    [SerializeField] private float Patton01_CoolTime = 0;

    [Header("사운드")]
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip DisAppearSound;

    [Header("Item")]
    [SerializeField] private GameObject Fianl_Item;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OnPatton_02 = false;
        OnStun = false;
        OnPatton_01_Move = false;
        animator.Play("Final_Boss_Idle");
    }

    private void OnEnable()
    {
        OnPatton_02 = false;
        OnPatton_01 = false;
        OnStun = false;
        Patton01_CoolTime = 0;
        time = 0;
        OnPatton_01_Move = false;
        ResetEffect();
        Patton_02();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnPatton_02 && !OnStun) // 패턴중이 아니면
        {
            //패턴을 시작한다.
            time += Time.deltaTime; // 시간추가.

            if (term < time) // 패턴과 패턴사이의 시간
            {
                Patton_02();
                time = 0;
            }
        }

        if (OnPatton_02) // 패턴 2 중일때 패턴이 끝났는가 확인
        {
            Patton01_CoolTime += Time.deltaTime;
            if (Patton01_Term < Patton01_CoolTime && !OnPatton_01)
            {
                StartCoroutine("Patton01");
            }


            OnPatton_02 = !this.gameObject.transform.GetChild(1).gameObject.GetComponent<FinalBoss_Patton_02>().isEndPatton_02();// 패턴이 끝났는가 확인
            if (!OnPatton_02 && !OnPatton_01) // 패턴이 끝났으면
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

    public void ResetEffect()
    {
        Patton01_Appear_Pos00.transform.GetChild(0).gameObject.SetActive(false);
        Patton01_Appear_Pos01.transform.GetChild(0).gameObject.SetActive(false);
        Patton01_Appear_Pos02.transform.GetChild(0).gameObject.SetActive(false);
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine("Death");
        Sound.instance.Play(clearSound, false);
        //아이템을 드랍한다.
    }
    IEnumerator Death()
    {
        animator.Play("Final_Boss_Die");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false); // 보스 비활성화
        Fianl_Item.SetActive(true);
    }

    #region Patton1
    IEnumerator Patton01()
    {
        OnPatton_01 = true;
        animator.Play("Final_Boss_Disappear");
        Sound.instance.Play(DisAppearSound, false);
        yield return new WaitForSeconds(0.7f); // 애니메이션
        SetBeria(false);

        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                this.gameObject.transform.position = Patton01_Appear_Pos00.transform.position; // 맵 옆으로 이동
                Warning_Sign = Patton01_Appear_Pos00.transform.GetChild(0).gameObject;
                break;
            case 1:
                this.gameObject.transform.position = Patton01_Appear_Pos01.transform.position; // 맵 옆으로 이동
                Warning_Sign = Patton01_Appear_Pos01.transform.GetChild(0).gameObject;
                break;
            case 2:
                this.gameObject.transform.position = Patton01_Appear_Pos02.transform.position; // 맵 옆으로 이동
                Warning_Sign = Patton01_Appear_Pos02.transform.GetChild(0).gameObject;
                break;
        }
        Sound.instance.Play(moveClip, false);

        Warning_Sign.SetActive(true); // 전조증상
        yield return new WaitForSeconds(1); // 애니메이션    
        Warning_Sign.SetActive(false);

        OnPatton_01_Move = true; // 움직임
        animator.Play("Final_Boss_Patton_01");
        yield return new WaitForSeconds(Patton01_Time); // 움직이는 시간


        OnPatton_01_Move = false; // 다시정리
        this.gameObject.transform.position = Boss_Spawn_Pos.transform.position;
        animator.Play("Final_Boss_Appear");
        yield return new WaitForSeconds(0.5f); // 애니메이션

        animator.Play("Final_Boss_Idle");
        OnPatton_01 = false;
        SetBeria(true);
        Patton01_CoolTime = 0; // 패턴 초기화
    }

    private void Patton01_Move()
    {
        this.gameObject.transform.position += new Vector3(-Patton01_Speed, 0, 0) * Time.deltaTime;
    }

    #endregion

    #region Patton2
    public override void Stun(bool _Stun)
    {
        base.Stun(_Stun);
    }

    private void Patton_02()
    {
        OnPatton_02 = true; // 패턴시작
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
