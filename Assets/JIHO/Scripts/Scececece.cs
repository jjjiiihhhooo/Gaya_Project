
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scececece : MonoBehaviour
{
    public GameObject fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) StartCoroutine(Cor());
    }

    private IEnumerator Cor()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("main2kimkyeomleveldsegin");
    }
}
