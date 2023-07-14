using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEEE : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("StartMap");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
