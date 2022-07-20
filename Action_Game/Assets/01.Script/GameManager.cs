using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    Stage1,
    Stage2,
    Stage3,
    Stage4,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<StageInfo> stageInfoList;
    public CoroutineManager stageCor = new CoroutineManager();

    List<string> enemyTypeList = new List<string>();
    Dictionary<string, int> monsterTypeDic = new Dictionary<string, int>();
    private int mobCount;
    [SerializeField] GameObject[] enemyPrefab;
    public GameObject[] EmemyPrefab => enemyPrefab;
    Coroutine cor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        foreach (string s in System.Enum.GetNames(typeof(MobType)))
        {
            enemyTypeList.Add(s);
        }
        for (int i = 0; i < /*System.Enum.GetValues(typeof(MobType)).Length*/2; i++)
        {
            MobPool.Instance.AddPool(enemyTypeList[i], enemyPrefab[i]);
        }

        stageCor.Execute(this,Mob_Spawn(stageInfoList[0]));
    }

    IEnumerator Mob_Spawn(StageInfo info)
    {
        foreach (var item in info.spawnDataList)
        {
            yield return new WaitForSeconds(item.time);
            var obj = MobPool.Instance.GetObject(item.type.ToString()).GetComponent<Monster>();
            obj.transform.position = Vector3.zero;
        }
    }
}


public class CoroutineManager
{
    Coroutine lastRoutine;

    public void Execute(MonoBehaviour owner,IEnumerator function)
    {
        if (lastRoutine != null)
            owner.StopCoroutine(lastRoutine);
        lastRoutine = owner.StartCoroutine(function);
    }
    public void Stop(MonoBehaviour owner)
    {
        if (lastRoutine != null)
            owner.StopCoroutine(lastRoutine);
    }

}