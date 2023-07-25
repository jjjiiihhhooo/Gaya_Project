using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngineInternal;

public class Controller2D : RaycastController
{
    float maxClimbAngle = 80; // �ö󰥶� ����
    float maxDescendAngle = 80; // �������� ����

    public CollisionInfo collisions; // collision����

    public override void Start()
    {
        base.Start();
    }

    public void Move(Vector3 velocity) //�����δ�.
    {
        UpdateRaycastOrigins(); // ���̾ƿ��� ������ ����
        collisions.Reset(); // ���� �ʱ�ȭ
        collisions.velocityOld = velocity;

        if(velocity.y < 0)
        { 
            DescendSlope(ref velocity);
        }
        if (velocity.x != 0){
            HorizontalCollisions(ref velocity); // �¿� ������
        }
        if (velocity.y != 0){
            VerticalCollisions(ref velocity); // ���� ������
        }

        transform.Translate(velocity); // ����� ��ġ��ŭ �����δ�.
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

            if (hit) //������ ��Ҵ�.
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // ������ ���� �������������� ����, (0,1)

                if(i == 0 && slopeAngle <= maxClimbAngle) // for ù��°�� �ѹ������, ������ �ִ밢�� �̻��ΰ�.
                {
                    float distanceToSlopeStart = 0; // ������ ������ ����
                    if(slopeAngle != collisions.slopeAngleOld)
                    {
                        if (collisions.descendingSlope)
                        {
                            collisions.descendingSlope = false;
                            velocity = collisions.velocityOld;
                        }
                        distanceToSlopeStart = hit.distance - skinWidth; // ���°�����ŭ ����Ѵ�.
                        velocity.x -= distanceToSlopeStart * directionX; // ������ŭ ���ش�.
                    }
                    ClimbSlope(ref velocity, slopeAngle); // �̵��ϰ��� �ϴ� �����, ������ ���� ���
                    velocity.x += distanceToSlopeStart * directionX; // ���� ������ŭ �ٽ� �����ش�.
                }

                if(!collisions.climbingSlope || slopeAngle > maxClimbAngle) { // ���� Ÿ�� �ִ°�? , �ִ������ �����ʰ��ΰ�?
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
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; // ������ ����
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask); // ����, ����, ����, ���̾��ũ
            
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) // ������ ��������
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

        if (collisions.climbingSlope) //���θ� ���� ���� ������ �ٸ� ���θ� �ö� ���
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

    void ClimbSlope(ref Vector3 velocity, float slopeAngle) // �ö󰥶�
    {
        float moveDistance = Mathf.Abs(velocity.x); // �������� ���� �����ȯ�ʿ� �¿� �̵���
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance; // ���� ������� �̵����� ���
        
        if( velocity.y <= climbVelocityY ) // ����� ��??
        {
            velocity.y = climbVelocityY; // ����Ѱ� �־��ֱ�
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x); // ����ؼ� �־��ֱ�
            collisions.below = true; // ������ �ִµ����� ���� ���� �������
            collisions.climbingSlope = true; // �ö󰡴����ΰ�?
            collisions.slopeAngle = slopeAngle; //�ö󰡴� ��������
        }
    }

    void DescendSlope(ref Vector3 veloity) // ��������
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

                        collisions.slopeAngle = slopeAngle; // ���� ���� ����
                        collisions.descendingSlope = true; // �������� ����
                        collisions.below = true; // ���� ���� ��� ����
                    }
                 }
            }
        }
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