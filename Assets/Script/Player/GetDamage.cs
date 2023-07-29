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

    [Header("�����ð� ���� * 0.2��")]
    public float isInvincibilityTime = 10;


    [Header("-------------------------�÷��̾��� ����------------------------- ")]
    public bool isHit;
    public bool isInvincibility;
    //������Ʈ
    Player_Move player_Move;
    Controller2D controller;
    Animator animator;



    private HpUI UIUpdate;

    private void Start()
    {
        UIUpdate = GameObject.FindAnyObjectByType<HpUI>();
        controller = GetComponent<Controller2D>();
        player_Move = GetComponent<Player_Move>();
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if(isHit && controller.collisions.below) // ������ ���� ������
        {
            revive();
        }
    }

    IEnumerator Invincibility()
    {
        for(int i = 0; i< 10; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(123, 123, 123, 255);
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
        }
        isInvincibility = false;
    }

    public void OnHit()
    {
        if (!isInvincibility)
        {
            Debug.Log("Player : ������!");
            PlayerStatus.Instance.SetHp(-1); // �������� �Դ´�.

            isInvincibility = true;
            UIUpdate.UpdateUI(PlayerStatus.Instance.HP); // UI������Ʈ

            KnockbackY = Mathf.Abs(KnockbackY); // ������ ���

            player_Move.isStun = true; // �Է��� ���ް��Ѵ�.
            isHit = true; // �¾Ҵ�!
            player_Move.input = Vector2.zero; // ����Ǵ� �������� �����.

            controller.collisions.below = false; // ���߿� ����
            player_Move.velocity = new Vector2(KnockbackX, KnockbackY); // �ش�������� ������.

            StartCoroutine("WaitStunTime");
        }
    }

    IEnumerator WaitStunTime()
    {
        StartCoroutine("Invincibility");
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(onHitTime);
        animator.SetBool("isDamage", false); // ������ �����Ѵ�.
    }


    public void revive()
    {
        Debug.Log("���� ��Ҵ�!");
        controller.collisions.below = true; // ���� �پ���.
        player_Move.isStun = false;
        isHit = false;
    }
}