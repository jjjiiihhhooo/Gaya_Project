using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBoss : Enemy
{
    [Header("���� ����ǥ��")]
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
        if (!OnPatton_01 && !OnPatton_02 && !OnStun) // �������� �ƴϸ�
        {
            //������ �����Ѵ�.
            //Patton_02();
            //StartCoroutine("Patton01");
        }

        if (OnPatton_02) // ���� 2 ���϶� ������ �����°� Ȯ��
        {
            OnPatton_02 = !this.gameObject.transform.GetChild(1).gameObject.GetComponent<FinalBoss_Patton_02>().isEndPatton_02();// ������ �����°� Ȯ��
            if (!OnPatton_02) // ������ ��������
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
        yield return new WaitForSeconds(0.7f); // �ִϸ��̼�
        this.gameObject.transform.position = Patton01_Appear_Pos.transform.position; // �� ������ �̵�
        // �������� ǥ���ʿ�
        yield return new WaitForSeconds(1); // �ִϸ��̼�
        OnPatton_01_Move = true; // ������
        animator.Play("Final_Boss_Patton_01");
        yield return new WaitForSeconds(Patton01_Time); // �����̴� �ð�
        OnPatton_01_Move = false; // �ٽ�����
        this.gameObject.transform.position = Boss_Spawn_Pos.transform.position;
        animator.Play("Final_Boss_Appear");
        yield return new WaitForSeconds(0.7f); // �ִϸ��̼�
        // �ٽ� ��Ÿ������.������ ��Ÿ������ ������°� �ٽ� ������ ��Ÿ������
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
