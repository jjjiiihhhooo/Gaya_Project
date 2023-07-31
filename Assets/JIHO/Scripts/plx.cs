
using UnityEngine;

public class plx : MonoBehaviour
{
    public Transform target;
    public float scrollAmount;
    public float moveSpeed;
    public Vector3 moveDir;
    public string name;

    public Transform right;

    private void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(name))
            transform.position = right.position;
    }
}
