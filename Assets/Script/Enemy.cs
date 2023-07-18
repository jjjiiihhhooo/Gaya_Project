using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform target; 
    public Rigidbody2D rb;

    [SerializeField] private float serchingDistance = 4;
    [SerializeField] private LayerMask layerMask;
    
    protected float curSpeed, attackDelay;
    protected bool isDie = false, goBack = false;
    

    public float currentHp; // ���� ü��
    public float maxHp; // �ִ� ü��
    private Animator eAnimator; // �ִϸ��̼� ��Ʈ�ѷ�

    public float moveDelay;
    public float currentDelay;
    public bool isMove;
    public bool isDamage;


    protected virtual void Start()
    {
        eAnimator = GetComponent<Animator>();
        currentHp = maxHp;
        curSpeed = 1.1f;
        attackDelay = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetDamage(float damage)
    {
        eAnimator.SetTrigger("Hit");
        currentHp -= damage;
        if (currentHp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        Vector2 nowPosition = new Vector2(this.gameObject.transform.position.x,this.gameObject.transform.position.y);

        RaycastHit2D LeftHit =  Physics2D.Raycast(nowPosition, Vector2.left, serchingDistance, layerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.left * serchingDistance, Color.red);

        RaycastHit2D RightHit = Physics2D.Raycast(nowPosition, Vector2.right, serchingDistance, layerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.right * serchingDistance, Color.red);
        if (LeftHit.collider.gameObject.layer == 3) //Ž���� �÷��̾ ���� �Ǿ�����
        {
            target = LeftHit.collider.gameObject.transform;
            eAnimator.SetBool("Walk",true);
        }
        if (RightHit.collider.gameObject.layer == 3)
        {
            target = LeftHit.collider.gameObject.transform;
            eAnimator.SetBool("Walk",true);
        }
        if(!LeftHit && !RightHit)
        {
            eAnimator.SetBool("Walk", false);
        }
        Move();
    }

    

    protected virtual void Move()
    {
    }
}
