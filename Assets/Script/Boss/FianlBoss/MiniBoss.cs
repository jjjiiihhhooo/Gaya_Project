using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    private float SpinSpeed;

    private void OnEnable()
    {
        currentHp = maxHp; // ü��ȸ��
        SpinSpeed = gameObject.transform.parent.GetComponent<FinalBoss_Patton_02>().SpinSpeed; // ���� �ӵ� ��������
        this.gameObject.transform.rotation = Quaternion.identity;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false); // ����Ʈ����
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }

    public override void Stun(bool _Stun)
    {
        base.Stun(_Stun);
    }

    private void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, -SpinSpeed) * Time.deltaTime);
    }
}
