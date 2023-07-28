using UnityEngine;

public class AttackCol : MonoBehaviour
{
    Enemy co;
    private void OnTriggerEnter2D(Collider2D collision) //공격 영역에 들어온것
    {
        if(collision.CompareTag("Enemy"))
        {
            co = collision.GetComponent<Enemy>();
            if (co != null)
            {
                collision.GetComponent<Enemy>().GetDamage(PlayerStatus.Instance.AtkDamage);
            }
        }
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Enemy>().GetDamage(PlayerStatus.Instance.AtkDamage);
        }
    }
}

/*
        if (collision.CompareTag("Boss"))
        {
            Sound.instance.Play(Sound.instance.audioDictionary["Hit"], false);
            player.effect.transform.localScale = new Vector3(2,2,2);
            player.effect.SetActive(false);
            player.effect.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 5);
            player.effect.SetActive(true);
            collision.GetComponent<Boss>().BossGetDamage(damage);
        }

        if(collision.CompareTag("mable"))
        {
            Sound.instance.Play(Sound.instance.audioDictionary["Hit"], false);
            player.effect.transform.localScale = new Vector3(2, 2, 2);
            player.effect.SetActive(false);
            player.effect.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 5);
            player.effect.SetActive(true);
            collision.GetComponent<marble>().GetDamage();

        }

        if (collision.CompareTag("lastBoss"))
        {
            if (!collision.GetComponent<LastBoss>().isAttack) return;
            Sound.instance.Play(Sound.instance.audioDictionary["Hit"], false);
            player.effect.transform.localScale = new Vector3(2, 2, 2);
            player.effect.SetActive(false);
            player.effect.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 5);
            player.effect.SetActive(true);
            collision.GetComponent<LastBoss>().GetDamage();
        }*/
