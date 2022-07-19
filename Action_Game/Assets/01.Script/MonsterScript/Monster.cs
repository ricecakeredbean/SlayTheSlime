using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobType
{
    slime,
    BigSlime,
    SkeletonWarrior,
    BronzeKnight,
    SliverKnight,
    GoldKnight,
    Baal,
    MaMon
}

public class Monster : MonoBehaviour
{
    #region Mob_Varible
    private MobType mType;
    public MobType MType
    {
        get => mType;
        set
        {
            switch(value)
            {
                case MobType.slime:
                    Hp = 5;
                    damage = 1;
                    break;
            }
            mType = value;
        }
    }
    private int hp;
    public int Hp
    {
        get => hp;
        set => hp = value;
    }
    private int damage;
    public int Damage => damage;
    private MonsterState currentState;
    private float moveSpeed = 3f;
    public float MoveSpeed => moveSpeed;
    Animator anim;
    public Animator Anim => anim;
    Vector3 dir;
    public Vector3 Dir => dir;
    float dis;
    public float Dis
    {
        get => dis;
        set => dis = value;
    }
    private string key;
    public string Key
    {
        get => key;
        set => key = value;
    }
    Dictionary<string, MonsterState> monsterDic = new Dictionary<string, MonsterState>();
    #endregion

    public void SetState<T>(string key) where T : MonsterState,new()
    {
        if(!monsterDic.ContainsKey(key))
        {
            Debug.Log($"MobAdded {key}");
            monsterDic.Add(key, new T());
        }
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = monsterDic[key];
        currentState.OnEnter(this);
    }

    protected virtual void Awake()
    {
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        currentState.Update();
        dir = Player.Instance.transform.position - transform.position;
        Dis = Vector3.Distance(transform.position, Player.Instance.transform.position);
    }
}
