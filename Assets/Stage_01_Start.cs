using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Stage_01_Start : MonoBehaviour
{
    private GameObject player;
    [Header("카메라연결")]
    [SerializeField] private GameObject VertualCamera;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.position = this.transform.position;
        VertualCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}
