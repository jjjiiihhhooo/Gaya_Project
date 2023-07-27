using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniBoss_Shoot : MonoBehaviour
{
    [Header("�Ѿ� ������Ʈ")]
    public GameObject Fire_Bullet;
    public GameObject Bullets; // �Ѿ˺����� �θ�ü

    [Header("�Ѿ� ��� �����̽ð�")]
    public float ShootTime = 1;


    [Header("�����¿� or �밢��")]
    public bool Upside = false;

    // ����
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
        //�����¿�
        GameObject bullet = Instantiate(Fire_Bullet, Bullets.transform); // ��ȯ
        bullet.transform.position = this.gameObject.transform.position; // ��ġ
        bullet.GetComponent<MiniBoss_Bullet>().vector3 = vector;
    }
}
