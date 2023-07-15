using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public marble[] marbles;

    public void Rotation()
    {
        StartCoroutine(RotateCor());
        for (int i = 0; i < marbles.Length; i++)
        {
            marbles[i].isRotate = true;
        }
        
    }

    private IEnumerator RotateCor()
    {
        float time = 0;
        while(time < 10)
        {
            transform.Rotate(new Vector3(0, 0, 1), 5);
                time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        
    }
}
