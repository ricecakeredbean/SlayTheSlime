using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Varible
    PlayerState currentState;
    [SerializeField] float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;
    Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    Dictionary<string, PlayerState> playerStateDic = new Dictionary<string, PlayerState>();
    #endregion
    public void SetState<T>(string key) where T : PlayerState, new()
    {
        if(!playerStateDic.ContainsKey(key))
        {
            Debug.Log($"ADDKey{key}");
            playerStateDic.Add(key, new T());
        }
        currentState = playerStateDic[key];
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        SetState<PlayerIdleState>(nameof(PlayerIdleState));
        InputManager.Instance.attackEvent += () => SetState<PlayerAttackState>(nameof(PlayerAttackState));
        InputManager.Instance.attackEvent += () => SetState<PlayerAttackState>(nameof(PlayerMoveState));
    }
    private void Update()
    {
        currentState.Update();
    }
}
