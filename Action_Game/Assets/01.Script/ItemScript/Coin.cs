using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Gold += 100;
            ReturnItem();
        }
    }
}
