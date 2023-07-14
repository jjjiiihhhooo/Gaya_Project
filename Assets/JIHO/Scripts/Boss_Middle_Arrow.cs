using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Boss_Middle_Arrow : MonoBehaviour
{
    public bool downStop;
    public Vector3 startPos;
    Animator anim;

    private void OnEnable()
    {
        downStop = false;
        anim = GetComponent<Animator>();
        transform.position = startPos;
    }

    private void Update()
    {
        if(!downStop)
        {
            transform.position += Vector3.down * 15 * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform"))
        {
            downStop = true;
            Effect();
        }

        if(collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().GetDamage(this.transform);
        }
    }

    public void Effect()
    {
        anim.SetTrigger("Effect");
        StartCoroutine(SetCor());
    }

    private IEnumerator SetCor()
    {
        yield return new WaitForSeconds(1f);
        SetActive();
    }

    public void SetActive()
    {
        this.gameObject.SetActive(false);
    }
}
