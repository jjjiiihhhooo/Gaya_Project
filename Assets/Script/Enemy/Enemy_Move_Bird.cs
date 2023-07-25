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
    [Header("�÷��̾� Ž�� �� Ž�� ����")]
    [SerializeField] private LayerMask PlayerlayerMask;
    [SerializeField] private float DetectPlayerDistance = 0.5f;

    [Header("���� ����")]
    [SerializeField] bool inAttack;

    [Header("�������� �ö󰡴� �ӵ� �� �������� �ð�")]
    [SerializeField] float attackSpeed;
    [SerializeField] float attackTime;

    // ������Ʈ
    private Animator animator;
    private SpriteRenderer sprite;

    //�̵�����
    [Header("�翷������ �̵�����")]
    [SerializeField] float sidewith;
    [SerializeField] float MoveSpeed;

    //����
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

        if (inAttack) // ������
        {
            Move_Attack(OnUp);
        }
        else // ����
        {
            Move();
        }
    }

    public void Move()
    {
        if(this.gameObject.transform.position.x > startPos.x + sidewith) // ������ġ + �������� ���� �������̸�
        {
            isRight = false;
        }
        if(this.gameObject.transform.position.x < startPos.x - sidewith) // ������ġ - �������� ���� �����̸�
        {
            isRight = true;
        }

        if(isRight)
        {
            this.gameObject.transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime; // �������̵�
            sprite.flipX = true;
        }
        else
        {
            this.gameObject.transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime; // �����̵�
            sprite.flipX = false;
        }
    }

    public void Move_Attack(bool onUp)
    {
        if(onUp)
        {
            this.gameObject.transform.position += new Vector3(0, attackSpeed, 0) * Time.deltaTime; // ���
        }
        else
        {
            this.gameObject.transform.position += new Vector3(0, -attackSpeed, 0) * Time.deltaTime; // �ϰ�
        }
    }

    public void DetectPlayer()
    {
        nowPosition = SetPos(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        RaycastHit2D GroundHit = Physics2D.Raycast(nowPosition, Vector2.down, DetectPlayerDistance, PlayerlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.down * DetectPlayerDistance, Color.blue);

        if (GroundHit.collider != null && !inAttack )
        {
            Debug.Log("�÷��̾ �߰��ߴ�!");
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        inAttack = true; // ������ �Ѵ�
        animator.SetBool("isAttack",true); // ���ݸ��
        OnUp = false;
        print("�ϰ�!");
        yield return new WaitForSeconds(attackTime); // �ϰ�
        OnUp = true;
        animator.SetBool("isAttack", false); // ����
        yield return new WaitForSeconds(attackTime); // ���
        inAttack = false;
    }
    
    public Vector2 SetPos(float pos_x, float pos_y)
    {
        return new Vector2(pos_x, pos_y);
    }
}
