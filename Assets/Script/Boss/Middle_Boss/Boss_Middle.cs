using System.Collections;
using UnityEngine;

public class Boss_Middle : Enemy
{
    /// <summary>
    /// ������ �����Ӱ� ���� ������ �����ϴ� Ŭ����
    /// </summary>
    
    [SerializeField] private StartBossStage startBossStage; // ���� ���۾˸���
    [SerializeField] private GameObject Patton02_Arrow;

    //����
    [Header("�������� ���� ���ǵ�, �̵��ð�")]
    [SerializeField] private float MoveSpeed = 1;
    [SerializeField] private float MoveRange = 1;
    
    private bool isMove = false; // ������
    private bool onPatton = false;
    private bool isClear = false;

    private void OnEnable()
    {
        onPatton = false;
        isMove = false;
        animator = GetComponent<Animator>(); // ������Ʈ ��������
        currentHp = maxHp; //ü�� ������
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
        isMove = true; //�̵�
        yield return new WaitForSeconds(MoveRange);
        isMove = false; // �̵�����
        this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX;
        animator.SetInteger("Patton01", 0);
        yield return new WaitForSeconds(1); // ���ð�
        onPatton = false;
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
        onPatton = true; // ���Ͻ���

        animator.SetBool("Patton02", true);
        yield return new WaitForSeconds(2.5f);
        animator.SetBool("Patton02", false);
        Patton02_Arrow.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        Patton02_Arrow.SetActive(false);

        onPatton = false; // ���ϳ�
    }

    #endregion

    public override void Die()
    {
        StopCoroutine("StartMiddleBoss");
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine("DieAnimation");
        
        startBossStage.RemoveWalls(); // �������� Ŭ����
    }

    IEnumerator DieAnimation()
    {
        animator.Play("Boss_Middle_Die");
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }
}
