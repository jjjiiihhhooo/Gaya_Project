using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBoss : Enemy
{
    [Header("���� ����ǥ��")]
    public bool OnPatton_02;
    public bool OnStun;
    public bool OnPatton_01_Move;
    public bool OnPatton_01;

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

    // ����
    [Header("�ð�����")]
    [SerializeField] private float time = 0;
    [SerializeField] private float Patton01_CoolTime = 0;

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
        if (!OnPatton_02 && !OnStun) // �������� �ƴϸ�
        {
            //������ �����Ѵ�.
            time += Time.deltaTime; // �ð��߰�.

            if (term < time) // ���ϰ� ���ϻ����� �ð�
            {
                Patton_02();
                time = 0;
            }
        }

        if (OnPatton_02) // ���� 2 ���϶� ������ �����°� Ȯ��
        {
            Patton01_CoolTime += Time.deltaTime;
            if (Patton01_Term < Patton01_CoolTime && !OnPatton_01)
            {
                StartCoroutine("Patton01");
            }


            OnPatton_02 = !this.gameObject.transform.GetChild(1).gameObject.GetComponent<FinalBoss_Patton_02>().isEndPatton_02();// ������ �����°� Ȯ��
            if (!OnPatton_02 && !OnPatton_01) // ������ ��������
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
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ
        //
        //�������� ����Ѵ�.
    }

    #region Patton1
    IEnumerator Patton01()
    {
        OnPatton_01 = true;
        animator.Play("Final_Boss_Disappear");
        yield return new WaitForSeconds(0.7f); // �ִϸ��̼�
        SetBeria(false);

        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                this.gameObject.transform.position = Patton01_Appear_Pos00.transform.position; // �� ������ �̵�
                Warning_Sign = Patton01_Appear_Pos00.transform.GetChild(0).gameObject;
                break;
            case 1:
                this.gameObject.transform.position = Patton01_Appear_Pos01.transform.position; // �� ������ �̵�
                Warning_Sign = Patton01_Appear_Pos01.transform.GetChild(0).gameObject;
                break;
            case 2:
                this.gameObject.transform.position = Patton01_Appear_Pos02.transform.position; // �� ������ �̵�
                Warning_Sign = Patton01_Appear_Pos02.transform.GetChild(0).gameObject;
                break;
        }


        Warning_Sign.SetActive(true); // ��������
        yield return new WaitForSeconds(1); // �ִϸ��̼�    
        Warning_Sign.SetActive(false);

        OnPatton_01_Move = true; // ������
        animator.Play("Final_Boss_Patton_01");
        yield return new WaitForSeconds(Patton01_Time); // �����̴� �ð�


        OnPatton_01_Move = false; // �ٽ�����
        this.gameObject.transform.position = Boss_Spawn_Pos.transform.position;
        animator.Play("Final_Boss_Appear");
        yield return new WaitForSeconds(0.5f); // �ִϸ��̼�

        animator.Play("Final_Boss_Idle");
        OnPatton_01 = false;
        SetBeria(true);
        Patton01_CoolTime = 0; // ���� �ʱ�ȭ
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
        OnPatton_02 = true; // ���Ͻ���
        animator.Play("Final_Boss_Patton_02");
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true); // 2������ ����
        SetBeria(true);
    }
    private void SetBeria(bool _Beria)
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(_Beria); // ����� ������ �ݶ��̴� ������
        this.gameObject.GetComponent<Collider2D>().enabled = !_Beria; // ����� ������ �ݶ��̴��� ������.
    }

    public IEnumerator Stun()
    {
        OnStun = true;
        SetBeria(false);
        animator.Play("Final_Boss_Stun_Ready");
        yield return new WaitForSeconds(0.5f);
        animator.Play("Final_Boss_Stun");
        yield return new WaitForSeconds(StunTime); //���Ͻð�
        OnStun = false;
        animator.Play("Final_Boss_Idle");
    }
    #endregion


}
