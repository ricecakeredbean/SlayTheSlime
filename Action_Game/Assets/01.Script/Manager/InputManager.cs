using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoSingleton<InputManager>
{
    Vector2 moveDir;
    public Vector2 MoveDir => moveDir;
    public Action attackEvent;
    public Action dashEvent;
    private void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(DataManager.keyData.attackKey))
        {
            attackEvent?.Invoke();
        }
        if(Input.GetKeyDown(DataManager.keyData.dashKEy))
        {
            dashEvent?.Invoke();
        }
    }
}
