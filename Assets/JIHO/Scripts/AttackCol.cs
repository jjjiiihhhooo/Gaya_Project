using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AttackCol : MonoBehaviour
{
    public float time;
    public float damage;
    public PlayerController player;

    private void OnEnable()
    {
        time = 0;
    }

    private void Update()
    {
        if (time < 0.1f) time += Time.deltaTime;
        else this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Sound.instance.Play(Sound.instance.audioDictionary["Hit"], false);
            player.effect.SetActive(false);
            player.effect.transform.position =new Vector3(collision.transform.position.x, collision.transform.position.y, 5);
            player.effect.SetActive(true);
            collision.GetComponent<Enermy>().GetDamage(damage);
        }
        if (collision.CompareTag("Boss"))
        {
            Sound.instance.Play(Sound.instance.audioDictionary["Hit"], false);
            player.effect.SetActive(false);
            player.effect.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 5);
            player.effect.SetActive(true);
            collision.GetComponent<Boss>().BossGetDamage(damage);
        }
    }
}
