using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float amountSpeed; // 배경 움직이는 속도
    public float scrollSpeed; // 스크롤 스피드

    public SpriteRenderer spriteRenderer; // 타겟이 되는 스프라이트
    public Material material; //offset을 변경할 해당 머테리얼
    public Vector2 newOffset;

    public float directionX;

    // Start is called before the first frame update
    private void Start()
    {
        scrollSpeed = amountSpeed; // 스크롤 스피드에 내가정한 스피드를 넣는다.
        material = spriteRenderer.material; // 스프라이트 랜더러가 가진 마테리얼을 넣는다.
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
