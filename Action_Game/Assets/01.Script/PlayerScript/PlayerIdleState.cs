using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Update()
    {
        if (InputManager.Instance.MoveDir.x != 0 || InputManager.Instance.MoveDir.y != 0)
        {
            
        }
    }
}
