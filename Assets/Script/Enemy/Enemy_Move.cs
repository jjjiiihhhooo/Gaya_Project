using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Move : MonoBehaviour
{
    public GameObject Target;
    public Vector3 StartPoint;
    public float MoveSpeed = 1;

    [SerializeField] private float serchingDistance = 4;
    [SerializeField] private LayerMask layerMask;

    private Animator eAnimator; // 애니메이션 컨트롤러
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position; // 시작위치
        eAnimator = GetComponent<Animator>(); // 애니메이션컨트롤러
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 nowPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

        RaycastHit2D LeftHit = Physics2D.Raycast(nowPosition, Vector2.left, serchingDistance, layerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.left * serchingDistance, Color.red);

        RaycastHit2D RightHit = Physics2D.Raycast(nowPosition, Vector2.right, serchingDistance, layerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.right * serchingDistance, Color.red);

        if (LeftHit.collider != null) //탐지에 플레이어가 감지 되었으면
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            Target = LeftHit.collider.gameObject;
            eAnimator.SetBool("Walk", true);
            this.gameObject.transform.position += Vector3.left * Time.deltaTime * MoveSpeed;
        }
        if (RightHit.collider != null)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            Target = RightHit.collider.gameObject;
            eAnimator.SetBool("Walk", true);
            this.gameObject.transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
        }
        if (!LeftHit && !RightHit)
        {
            eAnimator.SetBool("Walk", false);
        }
    }
}
