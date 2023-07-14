using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time < 0.1f) time += Time.deltaTime;
        else this.gameObject.SetActive(false);
    }
}
