
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEEE : MonoBehaviour
{
    public void LoadScene(string name = "StartMap")
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) SceneManager.LoadScene("Boss");
    }
}
