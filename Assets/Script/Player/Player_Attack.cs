using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Attack : MonoBehaviour
{
    [Header("���� ������")]
    public float attackDelay = 1.0f; // ���� ������

    [Header("���� �ҽ�")]
    public AudioClip attackSound = null;
    //������Ʈ
    Animator animator;

    //����
    private GameObject attackArea;
    private float currentDelay = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackArea = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // �Է°� �ޱ�
        AttackSide(input);
        currentDelay -= Time.deltaTime;
        currentDelay = Mathf.Clamp(currentDelay, 0, attackDelay);

        if (Input.GetKeyDown(KeyCode.Z) && currentDelay == 0) // ���ݹ�ư
        {
            StartCoroutine("Attack");
        }
    }

    private void AttackSide(float _input)
    {
        if (_input == -1)
        {
            attackArea.gameObject.transform.localPosition = new Vector3(-1, 0, 0);
        }
        else
        {
            attackArea.gameObject.transform.localPosition = new Vector3(1, 0, 0);
        }
    }

    IEnumerator Attack()
    {
        Sound.instance.Play(attackSound, false); // ����
        currentDelay = attackDelay; // ��Ÿ��
        Debug.Log("���ǵ�!");
        attackArea.SetActive(true); 
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.1f);

        attackArea.SetActive(false);
        animator.SetBool("isAttack", false);
    }
}