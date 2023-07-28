using Cinemachine;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject LifeCount_GameObject;
    [SerializeField] TextMeshProUGUI lifeCountText;
    [SerializeField] GameObject Player;
    private GameObject Camera;
    public UnityEvent ResetStage;

    public void Die()
    {
        Player.transform.position = PlayerStatus.Instance.SavePoint;
        StartCoroutine(DieCor());
        ResetStage.Invoke();
        Camera = FindAnyObjectByType(typeof(CinemachineVirtualCamera)).GameObject();
        Camera.GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }

    private IEnumerator DieCor()
    {
        LifeCount_GameObject.SetActive(true);
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
        LifeCount_GameObject.SetActive(false);
    }

}
