using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        Instance = this as T;
    }
    //public static T Instance
    //{
    //    get
    //    {
    //        if(!instance)
    //        {
    //            GameObject container = new GameObject();
    //            instance = container.AddComponent<T>();
    //        }
    //        return instance;
    //    }
    //}
}
