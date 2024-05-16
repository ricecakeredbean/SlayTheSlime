using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 

    protected void ReturnItem()
    {
        ObjPool.Instance.ReturnObject("item", this.gameObject);
    }
}
