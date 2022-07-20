using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
    Dictionary<string, Queue<Monster>> mobObjePools = new Dictionary<string, Queue<Monster>>();

    public Monster GetMonster(string key,GameObject MonsterPrefab)
    {
        Monster monster = null;
        if (mobObjePools.ContainsKey(key))
        {
            if (mobObjePools[key].Count > 0)
            {
                monster = mobObjePools[key].Dequeue();
                monster.gameObject.SetActive(true);
            }
            else
            {
                monster = Instantiate(MonsterPrefab).GetComponent<Monster>();
                monster.Key = key;
                monster.transform.SetParent(transform);
            }
        }
        else
        {
            mobObjePools.Add(key, new Queue<Monster>());

            monster = Instantiate(MonsterPrefab).GetComponent<Monster>();
            monster.Key = key;
            monster.transform.SetParent(transform);
        }
        return monster;
    }
    public void ReturnBullet(string key,)
}
