using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject out_obj;
    [SerializeField] TextMeshProUGUI lifeCountText;
    [SerializeField] GameObject Player;
    public UnityEvent ResetStage;

    public void Die()
    {
        Player.transform.position = PlayerStatus.Instance.SavePoint;
        StartCoroutine(DieCor());
        ResetStage.Invoke();
    }

    private IEnumerator DieCor()
    {
        out_obj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        lifeCountText.text = "L";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Li";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Lif";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life : ";
        yield return new WaitForSeconds(0.1f);
        lifeCountText.text = "Life : " + PlayerStatus.Instance.LifeCount.ToString();
        yield return new WaitForSeconds(0.7f);
        out_obj.SetActive(false);
    }

}
