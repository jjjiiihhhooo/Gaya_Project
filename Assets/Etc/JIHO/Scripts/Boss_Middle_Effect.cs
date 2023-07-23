
using System.Collections;
using UnityEngine;

public class Boss_Middle_Effect : MonoBehaviour
{
    public GameObject arrow;

    private void OnEnable()
    {
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        arrow.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
