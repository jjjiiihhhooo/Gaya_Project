using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Vector3 middleBoss_pos;
    public bool isMiddlePos;
    public bool isStart;


    public void Update()
    {
        if (!isStart) return;

        if (isMiddlePos)       
        {
            transform.position = middleBoss_pos;
        }
        else
        {
            if (!isMiddlePos && transform.position.x < -0.74)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -1), new Vector3(-0.73f, target.position.y + 1.2f, -1), Time.deltaTime * speed);
                
            }
            else
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -1), new Vector3(target.position.x, target.position.y + 1.2f, -1), Time.deltaTime * speed);
            }
        }
        
    }
}
