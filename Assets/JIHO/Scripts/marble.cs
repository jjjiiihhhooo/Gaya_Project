using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class marble : MonoBehaviour
{
    public Vector3 idlePos;
    public Vector3 attackPos;
    public Vector3 endPos;
    public LastBoss last;

    public float currentHp;
    public float maxHp;
    public float currentDelay;
    public float fireDelay;


    public bool isRotate;
    public bool isAttack;

    private void OnEnable()
    {
        isAttack = true;
        currentHp = maxHp;
    }

    private void Update()
    {
        FireDelay();
        if (isAttack) transform.position = Vector2.MoveTowards(transform.position, attackPos, 0.2f);
        else
        {
            if(currentDelay < 0)
            {
                currentDelay = fireDelay;
                GameObject temp = Instantiate(last.fireball, transform.position, quaternion.identity);
                temp.GetComponent<fireball>().dir = last.playerVec;
            }
            

        }

        if (Vector2.Distance(transform.position, attackPos) < 0.5f) isAttack = false;
    }

    public void FireDelay()
    {
        if (currentDelay >= 0) currentDelay -= Time.deltaTime;
    }

    public void GetDamage()
    {
        currentHp--;
        if(currentHp <= 0)
        {
            ResetPos();
        }
    }

    public void ResetPos()
    {
        StartCoroutine(Cor());
    }
   

    private IEnumerator Cor()
    {
        float time = 0;
        while(time < 1.2f)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector2.MoveTowards(transform.position, endPos, 0.1f);
            time += Time.deltaTime;
        }
        last.MarbleCount();
        maxHp++;
        this.gameObject.SetActive(false);
    }
}
