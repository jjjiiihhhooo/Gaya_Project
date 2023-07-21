using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private HpUI hpUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Sound.instance.Play(audio, false);
            PlayerStatus.Instance.SetHp(1);
            hpUI.UpdateUI(PlayerStatus.Instance.HP);
            this.gameObject.SetActive(false);
        }
    }
}
