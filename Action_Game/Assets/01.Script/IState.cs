using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState<T> where T : MonoBehaviour
{
    void OnEnter(T instnace);
    void Update();
    void OnExit();
}
#region Player
public class PlayerState : IState<Player>
{
    protected Player player;
    public virtual void OnEnter(Player player)
    {
        if (this.player == null)
            this.player = player;
    }
    public virtual void Update()
    {
    }
    public virtual void OnExit()
    {

    }
}

public class PlayerIdleState : PlayerState
{
    public override void Update()
    {
        player.Anim.SetBool("isRun", false);
        if (InputManager.Instance.MoveDir != Vector2.zero)
        {
            player.SetState<PlayerMoveState>(nameof(PlayerMoveState));
            player.Anim.SetBool("isRun", true);
        }
    }
}

public class PlayerMoveState : PlayerState
{
    public override void Update()
    {
        player.transform.Translate(new Vector3(InputManager.Instance.MoveDir.x * player.MoveSpeed, InputManager.Instance.MoveDir.y * player.MoveSpeed) * Time.deltaTime);
        if (InputManager.Instance.MoveDir.x != 0)
            player.transform.localScale = new Vector3(InputManager.Instance.MoveDir.x, player.transform.localScale.y, player.transform.localScale.z);
        if (InputManager.Instance.MoveDir.x == 0 && InputManager.Instance.MoveDir.y == 0)
        {
            player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
        }
    }
}
public class PlayerAttackState : PlayerState
{
    Coroutine cor;
    IEnumerator P_Attack()
    {
        yield return new WaitForSeconds(0.1f);
        player.weaponHit.SetActive(false);
        player.Anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.4f);
        player.Anim.SetBool("isAttack", false);
        player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        player.weaponHit.SetActive(true);
        if (cor != null)
            player.StopCoroutine(cor);
        cor = player.StartCoroutine(P_Attack());
    }
}
public class PlayerHitState : PlayerState
{
    Coroutine hitCor;
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        if (hitCor != null)
        {
            player.StopCoroutine(hitCor);
        }
        hitCor = player.StartCoroutine(P_Hit());
        player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }

    IEnumerator P_Hit()
    {
        player.PColider.enabled = false;
        player.Sprite.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.5f);
        player.Sprite.color = player.OriginColor;
        player.PColider.enabled = true;
    }
}
#endregion
#region Monster
public class MonsterState : IState<Monster>
{
    protected Monster monster;
    public virtual void OnEnter(Monster monster)
    {
        if (this.monster == null)
        {
            this.monster = monster;
        }
    }
    public virtual void Update()
    {

    }
    public virtual void OnExit()
    {

    }
}
public class MonsterIdleState : MonsterState
{
    public override void Update()
    {
        if (monster.Dis <= 5.5f)
        {
            monster.SetState<MonsterMoveState>(nameof(MonsterMoveState));
        }
        if (monster.Dis > 6f)
        {
            monster.SetState<MonsterthrowAttackState>(nameof(MonsterthrowAttackState));
        }
    }
}
public class MonsterMoveState : MonsterState
{
    public override void Update()
    {
        monster.Anim.SetBool("Walk", true);
        monster.Sprite.flipX = monster.transform.position.x > Player.Instance.transform.position.x;
        //몬스터 플레이어 방향으로 가기
        monster.transform.position = Vector3.MoveTowards(monster.transform.position, Player.Instance.transform.position, monster.MoveSpeed * Time.deltaTime);
        if (monster.Dis > 5.5f)
        {
            monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
        }
        if (monster.Dis <= 1.5f)
        { 
            monster.SetState<MonsterAttackState>(nameof(MonsterAttackState));
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        monster.Anim.SetBool("Walk", false);
    }
}
public class MonsterAttackState : MonsterState
{
    public override void OnEnter(Monster monster)
    {
        base.OnEnter(monster);
        monster.Attack();
        monster.Anim.SetBool("Attack", true);
    }
    public override void OnExit()
    {
        base.OnExit();
        monster.Anim.SetBool("Attack", false);
    }
}

public class MonsterthrowAttackState : MonsterState
{
    public override void Update()
    {
        if(monster.ThrowTime <= 0)
        {
            monster.ThrowAttack();
            monster.ThrowTime = 5;
        }
        monster.ThrowTime -= Time.deltaTime;
        monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
}

public class MonsterHitState : MonsterState
{
    Coroutine cor;
    public override void OnEnter(Monster monster)
    {
        base.OnEnter(monster);
        if (monster.Hp <= 0)
        {
            monster.SetState<MonsterDeadState>(nameof(MonsterDeadState));
            return;
        }
        if (cor != null)
        {
            monster.StopCoroutine(cor);
        }
        cor = monster.StartCoroutine(MHit());
    }
    IEnumerator MHit()
    {
        for (int i = 0; i < 2; i++)
        {
            monster.Sprite.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(0.2f);
            monster.Sprite.color = monster.OringColor;
        }
        monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
}
public class MonsterDeadState : MonsterState
{
    public override void OnEnter(Monster monster)
    {
        base.OnEnter(monster);
        monster.ReturnMe();
    }
}
#endregion