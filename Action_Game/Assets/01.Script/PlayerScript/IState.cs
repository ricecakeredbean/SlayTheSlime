using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState<T> where T : MonoBehaviour
{
    void Update();
}
