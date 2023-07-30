using UnityEngine;

public class FinalBoss_Patton_02 : MonoBehaviour
{
    public float radius = 2.0f;
    public float SpinSpeed = 10.0f;
    private void OnEnable() // 자리잡고 회전시키기
    {
        SetMiniBossPos();
        for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true); // 모든 게임오브젝트 살리기
        }
    }

    void Update() // 계속 회전시킨다.
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, SpinSpeed) * Time.deltaTime); // 계속 회전시킨다.
    }

    private void SetMiniBossPos() // 일정거리만큼 벌린다.
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

    public bool isEndPatton_02() // 패턴이 끝났는가?
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
