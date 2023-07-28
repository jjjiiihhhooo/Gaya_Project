using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }
}
