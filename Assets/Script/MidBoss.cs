using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBoss : MonoBehaviour
{
    public float mapScale, Delay;
    public int rushCount;
    public bool isRush, direction, isShot, isTurn;
    public bool testb;
    
    void Start()
    {
        mapScale = 10f;
        rushCount = 2;
        isRush = false;
        direction = false;
        isShot = false;
        isTurn = false;
    }

    void Update()
    {
        if (Delay < 0f)
        {
            if (rushCount > 0 && !isRush)
            {
                direction = (transform.position.x < 0);
                testb = direction;
                isRush = true;
            }
            else if (isRush)
            {
                Rush();
            }
        }
        else
        {
            Delay -= Time.deltaTime;
        }
        if (isShot)
        {
            shot();
        }
        if (isTurn)
        {

            Debug.Log("Turn");
        }
    }
    void Rush()
    {
        if (direction)
        {
            if (transform.position.x < (mapScale / 2))
            {
                transform.Translate(new Vector2(1, 0) * Time.deltaTime * 5);
                transform.localScale = new Vector3(5, 2, 1);
                Debug.Log("Rush!");
            }
            else
            {
                Delay = 1f;
                rushCount -= 1;
                isRush = false;
                isShot = true;
                Debug.Log("Shot");
            }
        }
        else
        {
            if (transform.position.x > -(mapScale / 2))
            {
                transform.Translate(new Vector2(1, 0) * Time.deltaTime * -5);
                transform.localScale = new Vector3(5, 2, 1);
                Debug.Log("Rush!");
            }
            else
            {
                Delay = 1f;
                rushCount -= 1;
                isRush = false;
                isShot = true;
                Debug.Log("Shot");
            }
        }
        if (rushCount == 0)
        {
            Delay = 3f;
            rushCount = 3;
            Debug.Log("Groggy");
        }
    }
    void shot()
    {
        
        isShot = false;
        isTurn = true;
    }
}
