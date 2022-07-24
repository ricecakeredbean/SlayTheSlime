using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobType
{
    Slime,
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
    public static Monster Instance;
    private MobType mType;
    public MobType MType
    {
        get => mType;
        set
        {
            switch(value)
            {
                case MobType.Slime:
                    hp = 5;
                    damage = 1;
                    thorTime = 99;
                    break;
                case MobType.BigSlime:
                    hp = 10;
                    damage = 3;
                    thorTime = 10;
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

    private float thorTime;
    public float ThrowTime
    {
        get => thorTime;
        set => thorTime = value;
    }

    private MonsterState currentState;
   
    private float moveSpeed = 3f;
    public float MoveSpeed => moveSpeed;

    private Animator anim;
    public Animator Anim => anim;

    private Vector3 dir;
    public Vector3 Dir => dir;

    private float dis;
    public float Dis => dis;

    private SpriteRenderer sprite;
    public SpriteRenderer Sprite
    {
        get => sprite;
        set => sprite = value;
    }
    private Color oringColor;
    public Color OringColor => oringColor;
    
    Dictionary<string, MonsterState> monsterDic = new Dictionary<string, MonsterState>();
    #endregion

    protected virtual void Awake()
    {
        Instance = this;
        sprite = GetComponent<SpriteRenderer>();
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
        anim = GetComponent<Animator>();
        oringColor = sprite.color;
    }
    public virtual void OnEnable()
    {
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
        sprite.color = oringColor;
        MType = MType;
    }
    public void SetState<T>(string key) where T : MonsterState,new()
    {
        if(!monsterDic.ContainsKey(key))
        {
            //Debug.Log($"MobAdded {key}");
            monsterDic.Add(key, new T());
        }
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = monsterDic[key];
        currentState.OnEnter(this);
    }


    private void Update()
    {
        currentState.Update();
        dir = Player.Instance.transform.position - transform.position;
        dis = Vector3.Distance(transform.position, Player.Instance.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PHit"))
        {
            Hit();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerHit(damage);
        }
    }
    public void ReturnMe()
    {
        MobPool.Instance.ReturnObject(System.Enum.GetName(typeof(MobType), MType),this.gameObject);
    }
    public virtual void Attack()
    {

    }
    public virtual void ThrowAttack()
    {

    }
    public void Hit()
    {
        hp -= Player.Instance.Damage;
        SetState<MonsterHitState>(nameof(MonsterHitState));
    }
}
