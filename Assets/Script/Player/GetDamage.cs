using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Player_Move),typeof(Controller2D),typeof(Animator))]
public class GetDamage : MonoBehaviour
{
    [Header("-------------------------���������� �κ�------------------------- ")]
    public float onHitTime = 2;
    public float KnockbackX = 5;
    public float KnockbackY = 10;

    [Header("-------------------------�÷��̾��� ����------------------------- ")]
    public bool isHit;

    //������Ʈ
    Player_Move player_Move;
    Controller2D controller;
    Animator animator;

    public HpUI UIUpdate;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        player_Move = GetComponent<Player_Move>();
        animator = GetComponent<Animator>();    }

    private void Update()
    {
        if(isHit && controller.collisions.below) // ������ ���� ������
        {
            revive();
        }
    }

    public void OnHit()
    {
        Debug.Log("Player : ������!");
        PlayerStatus.Instance.SetHp(-1); // �������� �Դ´�.

        UIUpdate.UpdateUI(PlayerStatus.Instance.HP); // UI������Ʈ

        KnockbackY = Mathf.Abs(KnockbackY); // ������ ���

        player_Move.isStun = true; // �Է��� ���ް��Ѵ�.

        isHit = true; // �¾Ҵ�!
        player_Move.input = Vector2.zero; // ����Ǵ� �������� �����.

        controller.collisions.below = false; // ���߿� ����
        player_Move.velocity = new Vector2(KnockbackX, KnockbackY); // �ش�������� ������.

        StartCoroutine("WaitStunTime");
    }

    IEnumerator WaitStunTime()
    {
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(onHitTime);
    }


    public void revive()
    {
        Debug.Log("���� ��Ҵ�!");
        controller.collisions.below = true; // ���� �پ���.
        player_Move.isStun = false;
        isHit = false;
        animator.SetBool("isDamage", false); // ������ �����Ѵ�.
    }
}