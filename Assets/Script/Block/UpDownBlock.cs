
using UnityEngine;

public class UpDownBlock : MonoBehaviour
{
    public float moveY;
    public float delayTime;
    public float curSpeed;
    public float curTime;
    private int UD;
    private float FY;

    private void Start()
    {
        UD = -1;
        FY = transform.position.y;
    }
    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            Move();
        }
    }

    void Move()
    {
        transform.Translate(new Vector2(0, UD) * Time.deltaTime * curSpeed);
        if (UD == -1)
        {
            if (FY - transform.position.y >= moveY)
            {
                UD  = 1;
                curTime = delayTime;
            }
        }
        if (UD == 1)
        {
            if (FY - transform.position.y <= 0)
            {
                UD = -1;
                curTime = delayTime;
            }
        }
    }
}
