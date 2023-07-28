using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    private float SpinSpeed;

    private void OnEnable()
    {
        currentHp = maxHp; // 체력회복
        SpinSpeed = gameObject.transform.parent.GetComponent<FinalBoss_Patton_02>().SpinSpeed; // 도는 속도 가져오기
        this.gameObject.transform.rotation = Quaternion.identity;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false); // 이펙트끄기
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
