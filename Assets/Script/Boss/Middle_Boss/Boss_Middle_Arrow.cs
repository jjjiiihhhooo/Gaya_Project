using System.Collections;
using UnityEngine;

public class Boss_Middle_Arrow : MonoBehaviour
{
    public Vector3 startPos;

    [Header("화살이 떨어지는 속도 및 시간")]
    [SerializeField] private float MoveSpeed = 20;
    [SerializeField] private float MoveTime = 0.33f;
    
    private Animator animator;
    private bool isMove;


    private void OnEnable()
    {
        startPos = transform.position;
        isMove = false;
        animator = GetComponent<Animator>();
        StartCoroutine("ShotArrow");
    }

    private void Update()
    {
        if(isMove)
        {
            this.gameObject.transform.position += Vector3.down * Time.deltaTime * MoveSpeed;
        }
    }

    private IEnumerator ShotArrow()
    {
        isMove = true;
        yield return new WaitForSeconds(MoveTime);
        isMove = false;
        animator.Play("Fire_Arrow_Damage");
        yield return new WaitForSeconds(3f);
        transform.position = startPos;
        this.gameObject.SetActive(false);
    }
}
