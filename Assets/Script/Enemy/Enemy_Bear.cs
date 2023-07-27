using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    public LayerMask PlayerMask;
    public float SerchIngDistance;

    //변수
    private bool onAttack = false;
    private void Awake()
    {
        onAttack = false;
        animator = GetComponent<Animator>();
    }

    public override void Stun(bool _Stun) // 데미지를 받았을경우
    {

    }

    public override void Die()
    {
        base.Die();
        this.gameObject.SetActive(false);
    }


   private void Update()
    {
        Vector2 nowPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.1f);
        RaycastHit2D LeftHit = Physics2D.Raycast(nowPosition, Vector2.left, SerchIngDistance, PlayerMask);
        Debug.DrawLine(nowPosition, nowPosition +  Vector2.left * SerchIngDistance, Color.green);
        RaycastHit2D RightHit = Physics2D.Raycast(nowPosition, Vector2.right, SerchIngDistance, PlayerMask);
        Debug.DrawLine(nowPosition, nowPosition + Vector2.right * SerchIngDistance, Color.green);
        if (LeftHit.collider != null)
        {
            if (LeftHit.collider.CompareTag("Player") && !onAttack)
            {
                Debug.Log("곰 : 플레이어다!");
                StartCoroutine("Attak");
            }
        }
        if(RightHit.collider != null)
        {
            if (RightHit.collider.CompareTag("Player") && !onAttack)
            {
                Debug.Log("곰 : 플레이어다!");
                StartCoroutine("Attak");
            }
        }
    }

    IEnumerator Attak()
    {
        onAttack = true;
        animator.SetInteger("isAttack", 1);
        yield return new WaitForSeconds(1);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        animator.SetInteger("isAttack", 2);
        yield return new WaitForSeconds(1);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        animator.SetInteger("isAttack", 0);
        onAttack = false;
    }
}
