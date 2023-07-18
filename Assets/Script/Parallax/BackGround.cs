using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BackGround : Parallax
{
    [SerializeField] Player player;
    float input;

    private void Update()
    {
        if(!player.isHit && !player.isStun)
        {
            input = Input.GetAxisRaw("Horizontal"); // 입력값 받기
        }
        else
        {
            input = 0;
        }
    }
    private void FixedUpdate()
    {
        MoveFrame(input);
    }
}
