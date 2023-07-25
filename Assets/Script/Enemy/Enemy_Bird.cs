using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bird : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
}
