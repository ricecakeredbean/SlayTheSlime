using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void Update()
    {
        player.Rigid.velocity = new Vector2(InputManager.Instance.MoveDir.x * player.MoveSpeed, InputManager.Instance.MoveDir.y * player.MoveSpeed);
    }
}
