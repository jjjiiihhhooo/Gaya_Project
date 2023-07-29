using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetItem : MonoBehaviour
{
    GameObject fade;
    private GameObject player;

    [Header("�ε��ؾ��ϴ� ���� �̸�")]
    [SerializeField] private string SceneName;

    [Header("��ε� �ؾ��ϴ� ���� �̸�")]
    [SerializeField] private string UnloadSceneName;

    [Header("�ִϸ��̼��̸� + ���� �ð�")]
    [SerializeField] private string Animation01;
    [SerializeField] private string Animation02;
    [SerializeField] private float AnimationTime01 = 5.0f;
    [SerializeField] private float AnimationTime02 = 5.0f;

    //Player_Get_Item_Middle01
    //Player_Get_Item_Middle02
    //Player_Get_Item_Final01
    //Player_Get_Item_Final02

    [Header("Ŭ����� ���������")]
    [SerializeField] GameObject gameover;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnEnable()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                gameover = GameObject.Find("GameManager");
                gameover.GetComponent<GameOver>().ResetStage.RemoveAllListeners();
                fade = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
                StartCoroutine("GetItemAnimation");
            }
        }
    }


    IEnumerator GetItemAnimation()
    {
        player.GetComponent<Animator>().Play(Animation01); // �÷��̾� �ִϸ��̼�
        yield return new WaitForSeconds(AnimationTime01);
        player.GetComponent<Player_Move>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;



        player.GetComponent<Animator>().Play(Animation02); // �÷��̾� �ִϸ��̼�
        yield return new WaitForSeconds(AnimationTime02);


        player.GetComponent<Player_Move>().enabled = true;
        player.GetComponent<Animator>().Play("Player_Idle");
        yield return new WaitForSeconds(1);
        fade.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadScene(UnloadSceneName);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        fade.SetActive(false);
    }
    // �÷��̾ ��� �ִϸ��̼� ǥ��
    // ������ ����
    // ���������� �Ѿ��.

}
