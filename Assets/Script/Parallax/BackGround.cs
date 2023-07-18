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
            input = Input.GetAxisRaw("Horizontal"); // �Է°� �ޱ�
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
