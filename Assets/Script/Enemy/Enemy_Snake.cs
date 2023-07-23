using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy_Move))]
public class Enemy_Snake : Enemy
{
    Enemy_Move enemy_Move;

    private void Awake()
    {
        enemy_Move = GetComponent<Enemy_Move>(); // 컴포넌트 가져오기
    }

    public override void Stun(bool _Stun)
    {
        base.Stun(_Stun);
        enemy_Move.enabled = !_Stun;
    }

    public override void Die()
    {
        base.Die();
        this.gameObject.SetActive(false);
    }
}
