using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    protected override void Awake()
    {
        base.Awake();
        MType = MobType.Slime;
    }
    public override void Attack()
    {
        StartCoroutine(MobAttack());
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
        SetState<MonsterIdleState>(nameof(MonsterIdleState));
    }
}
