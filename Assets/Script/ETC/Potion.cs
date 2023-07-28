using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Sound.instance.Play(audio, false);
            PlayerStatus.Instance.SetHp(1);
            this.gameObject.SetActive(false);
        }
    }
}
