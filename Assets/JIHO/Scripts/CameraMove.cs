using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMove : MonoBehaviour
{
    public Transform target;
    public float speed;

    public void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -1), new Vector3(target.position.x, target.position.y + 2, -1), Time.deltaTime * speed);
        
    }
}
