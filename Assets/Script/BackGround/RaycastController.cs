using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = 0.015f;
    public int horizontalRayCount = 4; // 세로 레이 갯수
    public int verticalRayCount = 4; // 가로 레이 갯수

    [HideInInspector]
    public float horizontalRaySpacing; // 세로 레이 간격
    [HideInInspector]
    public float verticalRaySpacing; // 가로 레이 간격

    [HideInInspector]
    public BoxCollider2D Collider;
    public RaycastOrigins raycastOrigins; // 레이캐스트 시작지점

    public virtual void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(skinWidth * -2); // bounds의 크기를 줄인다.

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); // 최소 2개 이상
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue); // 최소 2개이상 

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1); // 여백 공간
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1); // 여백공간
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds; // 오브젝트의 collider정보
        bounds.Expand(skinWidth * -2); // 

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

}
