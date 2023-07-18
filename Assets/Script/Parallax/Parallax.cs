using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float amountSpeed; // ��� �����̴� �ӵ�
    public float scrollSpeed; // ��ũ�� ���ǵ�

    public SpriteRenderer spriteRenderer; // Ÿ���� �Ǵ� ��������Ʈ
    public Material material; //offset�� ������ �ش� ���׸���
    public Vector2 newOffset;

    public float directionX;

    // Start is called before the first frame update
    private void Start()
    {
        scrollSpeed = amountSpeed; // ��ũ�� ���ǵ忡 �������� ���ǵ带 �ִ´�.
        material = spriteRenderer.material; // ��������Ʈ �������� ���� ���׸����� �ִ´�.
    }

    private void FixedUpdate()
    {
        MoveFrame(directionX);
    }

    public void MoveFrame(float _directionX)
    {
        newOffset = material.mainTextureOffset;
        newOffset.Set(newOffset.x + (scrollSpeed * _directionX) * Time.deltaTime, 0);
        material.mainTextureOffset = newOffset;
    }
}
