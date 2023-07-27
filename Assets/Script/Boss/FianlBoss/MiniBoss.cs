using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
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

        this.gameObject.transform.Rotate(new Vector3(0, 0, -50) * Time.deltaTime);
    }
}
