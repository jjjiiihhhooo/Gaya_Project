using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class FinalBoss_Patton_02 : MonoBehaviour
{
    public float radius = 2.0f;
    public float SpinSpeed = 10.0f;
    private void OnEnable() // �ڸ���� ȸ����Ű��
    {
        SetMiniBossPos();
        for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true); // ��� ���ӿ�����Ʈ �츮��
        }
    }

    void Update() // ��� ȸ����Ų��.
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, SpinSpeed) * Time.deltaTime); // ��� ȸ����Ų��.
    }

    private void SetMiniBossPos() // �����Ÿ���ŭ ������.
    {
        int numOfChild = transform.childCount;

        for (int i = 0; i < numOfChild; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / numOfChild;

            GameObject child = transform.GetChild(i).gameObject;

            child.transform.position
                = transform.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius;
        }
    }

    public bool isEndPatton_02() // ������ �����°�?
    {
        int numOfChild = transform.childCount;
        int count = 0;
        for (int i = 0; i < numOfChild; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeSelf == false)
            {
                count++;
            }
        }

        if (count == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
