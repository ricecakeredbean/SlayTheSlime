using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData
{
    public KeyCode attackKey = KeyCode.Mouse0;
}

public static class DataManager
{
    public static KeyData keyData = new KeyData();
}

