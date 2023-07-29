
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scececece : MonoBehaviour
{
    GameObject fade;
    public string SceneName;
    public string UnloadScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fade = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        if (collision.CompareTag("Player")) StartCoroutine(Cor());
    }

    private IEnumerator Cor()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName,LoadSceneMode.Additive);
        fade.SetActive(false);
    }
}
