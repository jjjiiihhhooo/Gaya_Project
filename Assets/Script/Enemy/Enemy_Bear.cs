using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bear : Enemy
{
    Enemy_Move enemy_Move;

    private void Awake()
    {
        enemy_Move = GetComponent<Enemy_Move>(); // ������Ʈ ��������
    }

    public override void Stun(bool _Stun) // �������� �޾������
    {

    }

    public override void Die()
    {
        base.Die();
        this.gameObject.SetActive(false);
    }
}
