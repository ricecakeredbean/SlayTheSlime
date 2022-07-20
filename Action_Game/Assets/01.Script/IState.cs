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
            player.Sprite.flipX = InputManager.Instance.MoveDir.x < 0;
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
        player.Anim.SetBool("isAttack", true);
        player.weaponHit.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        player.weaponHit.SetActive(false);
        player.Anim.SetBool("isAttack", false);
        player.SetState<PlayerIdleState>(nameof(PlayerIdleState));
    }
    public override void OnEnter(Player player)
    {
        base.OnEnter(player);
        if (cor != null)
            player.StopCoroutine(cor);
        cor = player.StartCoroutine(P_Attack());
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
    }
}
public class MonsterMoveState : MonsterState
{
    public override void Update()
    {
        //float deg = Mathf.Atan2(monster.Dir.y, monster.Dir.x) * Mathf.Rad2Deg;
        //monster.transform.rotation = Quaternion.Euler(new Vector3(0, 0, deg));
        monster.Sprite.flipX = monster.transform.position.x > Player.Instance.transform.position.x;
        //몬스터 플레이어 방향으로 가기
        monster.transform.position = Vector3.MoveTowards(monster.transform.position, Player.Instance.transform.position, monster.MoveSpeed * Time.deltaTime);
        if (monster.Dis > 5.5f)
            monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
        if (monster.Dis <= 1.2f)
        {
            monster.SetState<MonsterAttackState>(nameof(MonsterAttackState));
        }
    }
}
public class MonsterAttackState : MonsterState
{
    Coroutine cor;
    public override void OnEnter(Monster monster)
    {
        base.OnEnter(monster);
        if(cor != null)
        {
            monster.StopCoroutine(cor);
        }
        cor = monster.StartCoroutine(Mob_Attack());
    }
    IEnumerator Mob_Attack()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            yield return null;
        }
        Debug.Log("MobAttack(Hit)");
        monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
}
public class MonsterHitState : MonsterState
{
    Coroutine cor;
    IEnumerator MHit()
    {
        yield return null;
        monster.SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
    public override void OnEnter(Monster monster)
    {
        monster.Hp--;
        base.OnEnter(monster);
        if (monster.Hp <= 0)
        {
            monster.SetState<MonsterDeadState>(nameof(MonsterDeadState));
        }
        else if (cor != null)
        {
            monster.StopCoroutine(cor);
        }
        cor = monster.StartCoroutine(MHit());
    }
}
public class MonsterDeadState : MonsterState
{
    public override void OnEnter(Monster monster)
    {
        base.OnEnter(monster);
        monster.gameObject.SetActive(false);
    }
}
#endregion