using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    protected override void Awake()
    {
        MType = MobType.Slime;
        Key = "" + MType;
        base.Awake();
    }
}
