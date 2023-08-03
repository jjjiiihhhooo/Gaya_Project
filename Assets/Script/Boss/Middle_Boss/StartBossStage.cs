using Cinemachine;
using System.Collections;
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
    [SerializeField] private float KnockBackX = 2;
    [SerializeField] private float KnockBackY = 2;
    [SerializeField] private string soundName;
    //변수
    public bool isStart = false;
    private GameOver gameOver;



    public void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<GetDamage>().KnockbackX = KnockBackX;
        player.GetComponent<GetDamage>().KnockbackY = KnockBackY;
        BossStartText = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        gameOver = GameObject.Find("GameManager").GetComponent<GameOver>();
        gameOver.ResetStage.AddListener(ResetStartBossStage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isStart)
            {
                isStart = true;
                player.GetComponent<Player_Move>().enabled = false; // 못움직이게 함
                Sound.instance.Play(Sound.instance.audioDictionary[soundName], true);
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
        RemoveWalls(); // 벽치우기
        Boss.SetActive(false); // 보스끄기
        isStart = false; // 시작 다시
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
