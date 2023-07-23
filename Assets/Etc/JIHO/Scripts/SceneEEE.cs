
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEEE : MonoBehaviour
{
    public GameObject fade;
    public void LoadScene(string name = "Stage_1")
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) StartCoroutine(Cor());
    }

    private IEnumerator Cor()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Boss");
    }
}
