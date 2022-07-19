using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Varible
    public static Player Instance;
    private PlayerState currentState;
    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite => sprite;
    private Animator anim;
    public Animator Anim => anim;
    Dictionary<string, PlayerState> playerStateDic = new Dictionary<string, PlayerState>();
    #endregion
    public void SetState<T>(string key) where T : PlayerState, new()
    {
        if(!playerStateDic.ContainsKey(key))
        {
            Debug.Log($"Added {key}");
            playerStateDic.Add(key, new T());
        }
        if(currentState != null)
        {
            currentState.OnExit();
        }
        currentState = playerStateDic[key];
        currentState.OnEnter(this);
    }
    private void Awake()
    {
        Instance = this;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        SetState<PlayerIdleState>(nameof(PlayerIdleState));
        InputManager.Instance.attackEvent += () => SetState<PlayerAttackState>(nameof(PlayerAttackState));
    }
    private void Update()
    {
        currentState.Update();
    }
}
