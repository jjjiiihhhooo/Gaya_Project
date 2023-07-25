using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    Enemy_Move enemy_Move;

    private void Awake()
    {
        enemy_Move = GetComponent<Enemy_Move>(); // 컴포넌트 가져오기
    }

    public override void Stun(bool _Stun) // 데미지를 받았을경우
    {

    }

    public override void Die()
    {
        base.Die();
        this.gameObject.SetActive(false);
    }
}
