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
    /// ������ �����Ӱ� ���� ������ �����ϴ� Ŭ����
    /// </summary>
    
    [SerializeField] private StartBossStage startBossStage; // ���� ���۾˸���

    //������Ʈ
    private Animator animator; // �ִϸ��̼�


    //����
    [Header("�������� ���� ���ǵ�, �̵��ð�")]
    [SerializeField] private float MoveSpeed = 1;
    [SerializeField] private float MoveRange = 1;
    private bool isMove = false; // ������

    private void OnEnable()
    {
        isMove = false;
        animator = GetComponent<Animator>(); // ������Ʈ ��������
        currentHp = maxHp; //ü�� ������
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
        isMove = true; //�̵�
        yield return new WaitForSeconds(MoveRange);
        isMove = false; // �̵�����
        this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX;
        animator.SetInteger("Patton01", 0);
        yield return new WaitForSeconds(1); // ���ð�
    }

    private void Patton_01_Move()
    {
        if (!this.gameObject.GetComponent<SpriteRenderer>().flipX) // �����̵� �������̵�
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
        startBossStage.RemoveWalls(); // �������� Ŭ����
    }
}
