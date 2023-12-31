using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Animator))]
public class Enemy_Move : MonoBehaviour
{
    public Vector3 StartPoint;
    public float MoveSpeed = 1;

    [Header("좌우탐지범위, 중심에서 땅까지의 길이조절필요")]
    [SerializeField] private GameObject Target;
    [SerializeField] private float serchingDistance = 4;
    [SerializeField] private float GroundDistance = 0.5f;

    [Header("레이어 마스크")]
    [SerializeField] private LayerMask TargetlayerMask;
    [SerializeField] private LayerMask GroundlayerMask;
    Vector2 nowPosition;
    private Animator eAnimator; // 애니메이션 컨트롤러

    public virtual void Start()
    {
        StartPoint = transform.position; // 시작위치
        eAnimator = GetComponent<Animator>(); // 애니메이션컨트롤러
    }

    // Update is called once per frame
    private void Update()
    {
        nowPosition = SetPos(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        DetectGround();
        DetectPlayer();
    }

    public Vector2 SetPos(float pos_x, float pos_y)
    { 
        return new Vector2(pos_x,pos_y);
    }

    public void DetectGround()
    {
        RaycastHit2D GroundHit = Physics2D.Raycast(nowPosition, Vector2.down, GroundDistance, GroundlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.down * GroundDistance, Color.blue);

        if (GroundHit.collider == null)
        {
            this.gameObject.transform.position -= new Vector3(0, 0.01f, 0);
        }
    }

    public void DetectPlayer()
    {

        RaycastHit2D LeftHit = Physics2D.Raycast(nowPosition, Vector2.left, serchingDistance, TargetlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.left * serchingDistance, Color.red);

        RaycastHit2D RightHit = Physics2D.Raycast(nowPosition, Vector2.right, serchingDistance, TargetlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.right * serchingDistance, Color.red);


        if (LeftHit.collider != null) //탐지에 플레이어가 감지 되었으면
        {
            if (LeftHit.collider.CompareTag("Player"))
            {

                this.GetComponent<SpriteRenderer>().flipX = false;
                Target = LeftHit.collider.gameObject;
                eAnimator.SetBool("isMove", true);
                this.gameObject.transform.position += Vector3.left * Time.deltaTime * MoveSpeed;
            }
            else
            {
                this.gameObject.transform.position = this.gameObject.transform.position;
            }
        }
        if (RightHit.collider != null)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            Target = RightHit.collider.gameObject;
            eAnimator.SetBool("isMove", true);
            this.gameObject.transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
        }
        if (!LeftHit && !RightHit)
        {
            eAnimator.SetBool("isMove", false);
        }
    }
}
