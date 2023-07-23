using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartBossStage : MonoBehaviour
{
    [SerializeField] private GameObject[] Walls; // 시작시 생기는 벽
    [SerializeField] private GameObject player; // 플레이어
    [SerializeField] private GameObject BossStartText; // 시작시 보여지는 텍스트
    [SerializeField] private GameObject Boss; // 보스 오브젝트

    //변수
    public bool isStart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isStart)
            {
                isStart = true;
                player.GetComponent<Player_Move>().enabled = false; // 못움직이게 함
                StartCoroutine("ReadyMiddleBoss");
            }
        }
    }

    IEnumerator ReadyMiddleBoss()
    {
        BossStartText.SetActive(true);
        Walls[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        Walls[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        Walls[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        BossStartText.SetActive(false);
        player.GetComponent<Player_Move>().enabled = true;
    }

    public void ResetStartBossStage()
    {
        Debug.Log("aaaa");
    }

    public void RemoveWalls()
    {
        for(int i = 0; i < Walls.Length; i++)
        {
            Walls[i].gameObject.SetActive(false);
        }
    }
}
