using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PlatformController : RaycastController
{
    public LayerMask PassengerMask;
    public Vector3 move;


    [Header("최대한계 높이와 하한높이")]
    public float TopPosition;
    public float DownPosition;
    public float MoveSpeed;

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        UpdateRaycastOrigins();

        Vector3 velocity = move * Time.deltaTime;

        topDownPos();
        MovePassngers(velocity);
        transform.Translate(velocity);
    }

    void topDownPos()
    {
        if(this.gameObject.transform.position.y > TopPosition)
        {
            move.y = -MoveSpeed;
        }
        if(this.gameObject.transform.position.y < DownPosition)
        {
            move.y = MoveSpeed;
        }
    }

    void MovePassngers(Vector3 velocity)
    {
        HashSet<Transform> movedPassngers = new HashSet<Transform>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        //위아래로 움직이는 플랫폼
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; // 방향을 결정
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, PassengerMask); // 시작, 방향, 길이, 레이어마스크
                if (hit)
                {
                    if (!movedPassngers.Contains(hit.transform))
                    {
                    movedPassngers.Add(hit.transform);
                    float pushX = (directionY == 1) ? velocity.x : 0;
                    float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

                    hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }

        //가로로 움직이는 플랫폼
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, PassengerMask);

                if (hit)
                {
                    if (!movedPassngers.Contains(hit.transform))
                    {
                        movedPassngers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
                        float pushY = 0;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }

        // 승객이 플랫폼 위에 있으면
        if (directionY == -1 || velocity.y == 0 && velocity.x != 0)
        {
            float rayLength = skinWidth * 2;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, PassengerMask); // 시작, 방향, 길이, 레이어마스크
                
                if (hit)
                {
                    if (!movedPassngers.Contains(hit.transform))
                    {
                        Debug.Log("옴겨지는중");
                        movedPassngers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        hit.transform.Translate(new Vector3(pushX, pushY));
                    }
                }
            }
        }
    }
}
