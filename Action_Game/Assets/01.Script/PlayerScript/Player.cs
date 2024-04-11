using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private int damage = 1;
    public int Damage => damage;

    private int hp = 100;
    public int Hp => hp;

    public Sprite dieImage;

    private CapsuleCollider2D pColider;
    public CapsuleCollider2D PColider => pColider;

    public GameObject weaponHit;

    private PlayerState currentState;

    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;

    private SpriteRenderer sprite;
    public SpriteRenderer Sprite => sprite;

    private Animator anim;
    public Animator Anim => anim;

    private Color originColor;
    public Color OriginColor => originColor;

    Dictionary<string, PlayerState> playerStateDic = new Dictionary<string, PlayerState>();

    private void Awake()
    {
        Instance = this;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pColider = GetComponent<CapsuleCollider2D>();
        SetState<PlayerIdleState>(nameof(PlayerIdleState));
        InputManager.Instance.attackEvent += () => SetState<PlayerAttackState>(nameof(PlayerAttackState));
        InputManager.Instance.dashEvent += () => SetState<PlayerDashState>(nameof(PlayerDashState));
        originColor = sprite.color;
    }
    public void SetState<T>(string key) where T : PlayerState, new(){
        if (!playerStateDic.ContainsKey(key))
        {
            playerStateDic.Add(key, new T());
        }
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = playerStateDic[key];
        currentState.OnEnter(this);
    }
    private void Update()
    {
        currentState.Update();
    }

    public void PlayerHit(int mDamage)
    {
        if (hp <= 0)
        {
            SetState<PlayerDieState>(nameof(PlayerDieState));
        }
        else
        {
            hp -= mDamage;
            SetState<PlayerHitState>(nameof(PlayerHitState));
        }
    }
}
