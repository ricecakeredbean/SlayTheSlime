using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="StageData", fileName ="StageData")]
public class StageInfo : ScriptableObject
{
    public List<SpawnData> spawnDataList;
}

[Serializable]
public class SpawnData
{
    public float time;
    public MobType type;
}