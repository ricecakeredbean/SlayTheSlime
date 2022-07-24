using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="StageData", fileName ="StageData")]
public class StageInfo : ScriptableObject
{ 
    public List<SpawnData> spawnDataList;
    public float limitTime;
}

[Serializable]
public class SpawnData
{
    public float spawnTime;
    public MobType type;
}