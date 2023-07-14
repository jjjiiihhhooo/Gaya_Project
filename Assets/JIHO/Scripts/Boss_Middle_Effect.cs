using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Middle_Effect : MonoBehaviour
{
    public float time;
    public GameObject arrow;

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time < 2f)
        {
            time += Time.deltaTime;
        }
        else
        {
            arrow.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
