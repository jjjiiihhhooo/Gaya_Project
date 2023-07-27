using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBoss_Shoot : MonoBehaviour
{
    [Header("총알 오브젝트")]
    public GameObject Fire_Bullet;
    public GameObject Bullets; // 총알보관할 부모객체

    [Header("총알 쏘는 딜레이시간")]
    public float ShootTime = 1;


    [Header("상하좌우 or 대각선")]
    public bool Upside = false;

    // 변수
    private float time = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > ShootTime)
        {
            if (Upside)
            {
                shoot(Vector3.up);
                shoot(Vector3.left);
                shoot(Vector3.right);
                shoot(Vector3.down);
            }
            else
            {
                shoot(Vector3.up + Vector3.left);
                shoot(Vector3.up + Vector3.right);
                shoot(Vector3.down + Vector3.left);
                shoot(Vector3.down + Vector3.right);
            }
            time = 0;
        }
    }

    private void shoot(Vector3 vector)
    {
        //상하좌우
        GameObject bullet = Instantiate(Fire_Bullet, Bullets.transform); // 소환
        bullet.transform.position = this.gameObject.transform.position; // 배치
        bullet.GetComponent<MiniBoss_Bullet>().vector3 = vector;
    }
}
