using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float limitTime = 0;

    private int stage = 0;

    private bool isSpwan = false;

    private int mobDeath = 0;
    public int MobDeath
    {
        get => mobDeath;
        set => mobDeath = value;
    }

    public List<StageInfo> stageInfoList;
    public CoroutineManager stageCor = new CoroutineManager();

    List<string> enemyTypeList = new List<string>();
    [SerializeField] GameObject[] enemyPrefab;
    public GameObject[] EmemyPrefab => enemyPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        limitTime = stageInfoList[stage].limitTime;
        foreach (string s in System.Enum.GetNames(typeof(MobType)))
        {
            enemyTypeList.Add(s);
        }
        for (int i = 0; i < System.Enum.GetValues(typeof(MobType)).Length; i++)
        {
            MobPool.Instance.AddPool(enemyTypeList[i], enemyPrefab[i]);
        }

        stageCor.Execute(this, Mob_Spawn(stageInfoList[stage]));
    }

    private void Update()
    {

        if(limitTime > 0)
        {
            limitTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("GameOver");
        }
        if (isSpwan)
            SpawnCount();
    }

    private void SpawnCount()
    {
        if (stage == 0 && mobDeath == 0)
        {
            stage++;
            limitTime = stageInfoList[stage].limitTime;
            stageCor.Execute(this, Mob_Spawn(stageInfoList[stage]));
        }
    }

    IEnumerator Mob_Spawn(StageInfo info)
    {
        foreach (var item in info.spawnDataList)
        {
            yield return new WaitForSeconds(item.spawnTime);
            var obj = MobPool.Instance.GetObject(item.type.ToString()).GetComponent<Monster>();
            obj.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));
        }
        isSpwan = true;
    }
}


public class CoroutineManager
{
    Coroutine lastRoutine;

    public void Execute(MonoBehaviour owner, IEnumerator function)
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