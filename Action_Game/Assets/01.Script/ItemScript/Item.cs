using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{ 
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReturnItem();
        }
    }
    private void ReturnItem()
    {
        ObjPool.Instance.ReturnObject("item", this.gameObject);
    }
}
