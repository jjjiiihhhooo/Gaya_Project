
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerStatus.Instance.SavePoint = this.transform.position;
            anim.SetTrigger("save");
        }
    }
}
