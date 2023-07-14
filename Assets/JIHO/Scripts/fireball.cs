using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public Vector3 dir;
    public float time;

    private void Update()
    {
        if(time > 0)
        {
            Vector3 vec = dir - transform.position;
            transform.position += vec * 3 * Time.deltaTime;
            time -= Time.deltaTime; 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
