
using UnityEngine;

public class LeftRightBlock : MonoBehaviour
{
    public float moveX;
    public float delayTime;
    public float curSpeed;
    private float curTime;
    private int LR;
    private float FX;

    private void Start()
    {
        LR = -1;
        FX = transform.position.x;
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
        transform.Translate(new Vector2(LR, 0) * Time.deltaTime * curSpeed);
        if (LR == -1)
        {
            if (FX - transform.position.x >= moveX)
            {
                LR = 1;
                curTime = delayTime;
            }
        }
        if (LR == 1)
        {
            if (FX - transform.position.x <= 0)
            {
                LR = -1;
                curTime = delayTime;
            }
        }
    }
}
