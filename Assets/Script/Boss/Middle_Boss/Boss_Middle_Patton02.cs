using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Middle_Patton02 : MonoBehaviour
{
    public GameObject Effect_01;
    public GameObject Effect_02;
    public GameObject Effect_03;

    private void OnEnable()
    {
        Effect_01.SetActive(true);
        Effect_02.SetActive(true);
        Effect_03.SetActive(true);
    }
}
