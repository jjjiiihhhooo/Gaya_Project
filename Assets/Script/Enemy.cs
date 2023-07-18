using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform target; 
    public Rigidbody2D rb;

    [SerializeField] private float serchingDistance = 4;
    [SerializeField] private LayerMask layerMask;
    
    protected float curSpeed, attackDelay;
    protected bool isDie = false, goBack = false;
    

    public float currentHp; // 현재 체력
    public float maxHp; // 최대 체력
    private Animator eAnimator; // 애니메이션 컨트롤러

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
        if (LeftHit) //탐지에 플레이어가 감지 되었으면
        {
            eAnimator.SetBool("Walk",true);
        }
        if (RightHit)
        {
            eAnimator.SetBool("Walk",true);
        }
        if(!LeftHit && !RightHit)
        {
            eAnimator.SetBool("Walk", false);
        }
    }

    protected virtual void Move()
    {
        
    }
}
