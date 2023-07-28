using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Android;
using UnityEngine;

public class StartBossStage : MonoBehaviour
{
    [SerializeField] private GameObject[] Walls; // 시작시 생기는 벽
    private GameObject player; // 플레이어
    private GameObject BossStartText; // 시작시 보여지는 텍스트
    [SerializeField] private GameObject Boss; // 보스 오브젝트
    [SerializeField] private GameObject BossSpawnPoint;
    [SerializeField] private Transform BossCameraPos;
    [SerializeField] private GameObject Camera;
    //변수
    public bool isStart = false;

    public void Start()
    {
        player = GameObject.Find("Player");
        BossStartText = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isStart)
            {
                isStart = true;
                player.GetComponent<Player_Move>().enabled = false; // 못움직이게 함
                StartCoroutine("ReadyBoss");
            }
        }
    }

    IEnumerator ReadyBoss()
    {
        BossStartText.SetActive(true);
        Camera.GetComponent<CinemachineVirtualCamera>().Follow = BossCameraPos.transform;


        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
        }

        BossStartText.SetActive(false);
        player.GetComponent<Player_Move>().enabled = true;
        Boss.SetActive(true);
        Boss.transform.position = BossSpawnPoint.transform.position;
        Boss.GetComponent<SpriteRenderer>().flipX = false;
    }


    public void ResetStartBossStage()
    {
        RemoveWalls();
        Boss.SetActive(false);
        isStart = false;
    }

    public void RemoveWalls()
    {
        Camera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i].gameObject.SetActive(false);
        }
    }
}
