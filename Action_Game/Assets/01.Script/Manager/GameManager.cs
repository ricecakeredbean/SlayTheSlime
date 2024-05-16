using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region set
    public static GameManager Instance;

    public GameObject gameOverFloor;

    private int gold = 0;
    public int Gold
    {
        get => gold;
        set => gold = value;
    }

    private float limitTime = 0;
    public float LimitTime => limitTime;

    private int stage = 0;
    public int Stage
    {
        get => stage;
        set => stage = value;
    }

    private bool isSpwan = false;

    private int mobCount = 0;
    public int MobCount
    {
        get => mobCount;
        set => mobCount = value;
    }

    public List<StageInfo> stageInfoList;

    public CoroutineManager stageCor = new CoroutineManager();

    private List<string> enemyTypeList = new List<string>();


    [SerializeField] GameObject[] itemPrefab;
    public GameObject[] ItemPrefab => itemPrefab;

    [SerializeField] GameObject[] enemyPrefab;
    public GameObject[] EmemyPrefab => enemyPrefab;
    #endregion

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
            ObjPool.Instance.AddPool(enemyTypeList[i], enemyPrefab[i]);
        }
        stageCor.Execute(this, Mob_Spawn(stageInfoList[stage]));
        for (int i = 0; i < itemPrefab.Length; i++)
        {
            ObjPool.Instance.AddPool("item", itemPrefab[i]);

        }
    }

    private void Update()
    {
        if (limitTime > 0)
        {
            limitTime -= Time.deltaTime;
        }
        else
        {
            Player.Instance.SetState<PlayerDieState>(nameof(PlayerDieState));
        }

        if (isSpwan && mobCount == 0)
        {
            StageCheck();
        }
        UIManager.Instance.UIUpdate();
    }

    public void ItemSpawn(Transform spawntrans)
    {
        var item = ObjPool.Instance.GetObject("item");
        item.transform.position = spawntrans.position;
    }

    private void StageCheck()
    {
        if (stage == 0)
        {
            if (MapManager.clearStage < 1)
                MapManager.clearStage = 1;
            GameClear();
        }
    }

    public void GameClear()
    {
        UIManager.Instance.WinUi();
    }

    public void GameOver()
    { 
        gameOverFloor.SetActive(true);
        UIManager.Instance.GameOverUI();
    }

    IEnumerator Mob_Spawn(StageInfo info)
    {
        foreach (var item in info.spawnDataList)
        {
            yield return new WaitForSeconds(item.spawnTime);
            var obj = ObjPool.Instance.GetObject(item.type.ToString()).GetComponent<Monster>();
            obj.transform.position = new Vector2(Random.Range(-8, 8), Random.Range(-8, 8));
            mobCount++;
            Debug.Log(mobCount);
        }
        isSpwan = true;
        Debug.Log(isSpwan);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadBack()
    {
        SceneManager.LoadScene(0);
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
