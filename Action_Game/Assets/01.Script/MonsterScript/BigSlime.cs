using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : Monster
{
    protected override void Awake()
    {
        base.Awake();
        MType = MobType.BigSlime;
    }
    public override void Attack()
    {
        StartCoroutine(MobAttack());
    }
    public override void ThrowAttack()
    {
        Anim.SetBool("Attack", true);
        StartCoroutine(ThrowMob());
    }
    IEnumerator MobAttack()
    {
        Vector3 dash = Dir;
        yield return new WaitForSeconds(1f);
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            transform.Translate(dash * MoveSpeed * 2 * Time.deltaTime);
            yield return null;
        }
        Anim.SetBool("Attack", false);
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
    IEnumerator ThrowMob()
    {
        Vector3 dash = Dir;
        var obj = ObjPool.Instance.GetObject(System.Enum.GetName(typeof(MobType), MobType.Slime));
        obj.transform.position = transform.position;
        GameManager.Instance.MobCount++;
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            obj.transform.Translate(dash * MoveSpeed * Time.deltaTime);
            yield return null;
        }
        obj.transform.position = dash;
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
}
