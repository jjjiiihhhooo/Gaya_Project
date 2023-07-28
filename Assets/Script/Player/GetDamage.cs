using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Player_Move),typeof(Controller2D),typeof(Animator))]
public class GetDamage : MonoBehaviour
{
    [Header("-------------------------조절가능한 부분------------------------- ")]
    public float onHitTime = 2;
    public float KnockbackX = 5;
    public float KnockbackY = 10;

    [Header("-------------------------플레이어의 상태------------------------- ")]
    public bool isHit;

    //컴포넌트
    Player_Move player_Move;
    Controller2D controller;
    Animator animator;

    private HpUI UIUpdate;

    private void Start()
    {
        UIUpdate = GameObject.FindAnyObjectByType<HpUI>();
        controller = GetComponent<Controller2D>();
        player_Move = GetComponent<Player_Move>();
        animator = GetComponent<Animator>();    }

    private void Update()
    {
        if(isHit && controller.collisions.below) // 맞은뒤 땅을 밟으면
        {
            revive();
        }
    }

    public void OnHit()
    {
        Debug.Log("Player : 아프다!");
        PlayerStatus.Instance.SetHp(-1); // 데미지를 입는다.

        UIUpdate.UpdateUI(PlayerStatus.Instance.HP); // UI업데이트

        KnockbackY = Mathf.Abs(KnockbackY); // 무조건 양수

        player_Move.isStun = true; // 입력을 못받게한다.

        isHit = true; // 맞았다!
        player_Move.input = Vector2.zero; // 적용되던 움직임을 멈춘다.

        controller.collisions.below = false; // 공중에 띄운다
        player_Move.velocity = new Vector2(KnockbackX, KnockbackY); // 해당방향으로 날린다.

        StartCoroutine("WaitStunTime");
    }

    IEnumerator WaitStunTime()
    {
        animator.SetBool("isDamage", true);
        yield return new WaitForSeconds(onHitTime);
        animator.SetBool("isDamage", false); // 변수를 정리한다.
    }


    public void revive()
    {
        Debug.Log("땅에 닿았다!");
        controller.collisions.below = true; // 땅에 붙었다.
        player_Move.isStun = false;
        isHit = false;
    }
}