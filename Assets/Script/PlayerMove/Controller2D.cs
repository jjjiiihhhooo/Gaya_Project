using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngineInternal;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public LayerMask collisionMask;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4; // 세로 레이 갯수
    public int verticalRayCount = 4; // 가로 레이 갯수

    float maxClimbAngle = 80; // 올라갈때 각도
    float maxDescendAngle = 80; // 내려갈때 각도

    float horizontalRaySpacing; // 세로 레이 간격
    float verticalRaySpacing; // 가로 레이 간격

    BoxCollider2D Collider;
    RaycastOrigins raycastOrigins; // 레이캐스트 시작지점
    public CollisionInfo collisions; // collision정보


    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity) //움직인다.
    {
        UpdateRaycastOrigins(); // 레이아웃의 시작점 수정
        collisions.Reset(); // 정보 초기화
        collisions.velocityOld = velocity;

        if(velocity.y < 0)
        { 
            DescendSlope(ref velocity);
        }
        if (velocity.x != 0){
            HorizontalCollisions(ref velocity); // 좌우 움직임
        }
        if (velocity.y != 0){
            VerticalCollisions(ref velocity); // 상하 움직임
        }

        transform.Translate(velocity); // 계산한 위치만큼 움직인다.
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds; // 오브젝트의 collider정보
        bounds.Expand(skinWidth * -2); // 

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) //뭔가에 닿았다.
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // 경사로의 각도 맞은지점에서의 수직, (0,1)

                if(i == 0 && slopeAngle <= maxClimbAngle) // for 첫번째에 한번만계산, 각도가 최대각도 이상인가.
                {
                    float distanceToSlopeStart = 0; // 오르기 시작한 지점
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        if (collisions.descendingSlope)
                        {
                            collisions.descendingSlope = false;
                            velocity = collisions.velocityOld;
                        }
                        distanceToSlopeStart = hit.distance - skinWidth; // 남는공간만큼 계산한다.
                        velocity.x -= distanceToSlopeStart * directionX; // 공간만큼 빼준다.
                    }
                    ClimbSlope(ref velocity, slopeAngle); // 이동하고자 하는 방향과, 경사로의 각도 계산
                    velocity.x += distanceToSlopeStart * directionX; // 남는 공간만큼 다시 더해준다.
                }

                if(!collisions.climbingSlope || slopeAngle > maxClimbAngle) { // 벽을 타고 있는가? , 최대높이의 각도초과인가?
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.right = directionX == 1;
                    collisions.left = directionX == -1;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; // 방향을 결정
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask); // 시작, 방향, 길이, 레이어마스크
            
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) // 뭔가가 닿았을경우
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope) //경사로를 가는 도중 각도가 다른 경사로를 올라갈 경우
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1)? raycastOrigins.bottomLeft : raycastOrigins.bottomRight)+ Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle) // 올라갈때
    {
        float moveDistance = Mathf.Abs(velocity.x); // 움직임의 정도 양수변환필요 좌우 이동시
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance; // 위로 어느정도 이동할지 계산
        
        if( velocity.y <= climbVelocityY ) // 계산결과 비교??
        {
            velocity.y = climbVelocityY; // 계산한값 넣어주기
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x); // 계산해서 넣어주기
            collisions.below = true; // 오르고 있는동안은 발이 땅에 닿아있음
            collisions.climbingSlope = true; // 올라가는중인가?
            collisions.slopeAngle = slopeAngle; //올라가는 각도저장
        }
    }

    void DescendSlope(ref Vector3 veloity) // 내려갈때
    {
        float directionX = Mathf.Sign(veloity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                 if(Mathf.Sign(hit.normal.x) == directionX)
                 {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(veloity.x))
                    {
                        float moveDistance = Mathf.Abs(veloity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        veloity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(veloity.x);
                        veloity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle; // 경사로 각도 저장
                        collisions.descendingSlope = true; // 내려가고 있음
                        collisions.below = true; // 발이 땅에 닿고 있음
                    }
                 }
            }
        }
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(skinWidth * -2); // bounds의 크기를 줄인다.

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); // 최소 2개 이상
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue); // 최소 2개이상 

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1); // 여백 공간
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1); // 여백공간
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;

        public void Reset()
        {
            above = below =false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}