using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [Header("°ø°Ý ÄðÅ¸ÀÓ")]
    public float AtcCoolTime = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("onhit");
            PlayerStatus.Instance.HP -= 1;
            collision.gameObject.GetComponent<GetDamage>().OnHit();
            StartCoroutine("ICD");
        }
    }

    IEnumerator ICD()
    {
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
        yield return new WaitForSeconds(AtcCoolTime);
        this.gameObject.GetComponent<Collider2D>().isTrigger = true;
    }
}
