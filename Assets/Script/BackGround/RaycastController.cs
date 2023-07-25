using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = 0.015f;
    public int horizontalRayCount = 4; // ���� ���� ����
    public int verticalRayCount = 4; // ���� ���� ����

    [HideInInspector]
    public float horizontalRaySpacing; // ���� ���� ����
    [HideInInspector]
    public float verticalRaySpacing; // ���� ���� ����

    [HideInInspector]
    public BoxCollider2D Collider;
    public RaycastOrigins raycastOrigins; // ����ĳ��Ʈ ��������

    public virtual void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(skinWidth * -2); // bounds�� ũ�⸦ ���δ�.

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); // �ּ� 2�� �̻�
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue); // �ּ� 2���̻� 

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1); // ���� ����
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1); // �������
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds; // ������Ʈ�� collider����
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