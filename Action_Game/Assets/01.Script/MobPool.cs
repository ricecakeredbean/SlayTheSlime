using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPool : MonoBehaviour
{
    public static MobPool Instance;
    Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddPool(string key,GameObject prefab)
    {
        poolDic.Add(key, new PoolData(prefab));
    }

    public GameObject GetObject(string key)
    {
        PoolData data = poolDic[key];
        GameObject obj;
        if (data.stack.Count > 0)
        {
            obj = data.stack.Pop();
            obj.SetActive(true);
            obj.transform.SetParent(null);
        }
        else
        {
            obj = Instantiate(data.prefab);
        }
        return obj;
    }

    public void ReturnObject(string key,GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        poolDic[key].stack.Push(obj);
        GameManager.Instance.MobDeath++;
    }


}
public class PoolData
{
    public GameObject prefab;
    public Stack<GameObject> stack = new Stack<GameObject>();
    
    public PoolData(GameObject _prefab)
    {
        prefab = _prefab;
    }
}