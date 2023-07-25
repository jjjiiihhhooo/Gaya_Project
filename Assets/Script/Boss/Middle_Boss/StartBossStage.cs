using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Android;
using UnityEngine;

public class StartBossStage : MonoBehaviour
{
    [SerializeField] private GameObject[] Walls; // ���۽� ����� ��
    [SerializeField] private GameObject player; // �÷��̾�
    [SerializeField] private GameObject BossStartText; // ���۽� �������� �ؽ�Ʈ
    [SerializeField] private GameObject Boss; // ���� ������Ʈ
    [SerializeField] private GameObject BossSpawnPoint;
    [SerializeField] private Transform BossCamera;
    [SerializeField] private GameObject Camera;
    //����
    public bool isStart = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isStart)
            {
                isStart = true;
                player.GetComponent<Player_Move>().enabled = false; // �������̰� ��
                StartCoroutine("ReadyMiddleBoss");
            }
        }
    }

    IEnumerator ReadyMiddleBoss()
    {
        BossStartText.SetActive(true);
        Walls[0].gameObject.SetActive(true);
        Camera.GetComponent<CinemachineVirtualCamera>().Follow = BossCamera.transform;
        yield return new WaitForSeconds(1);

        Walls[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        Walls[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

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
