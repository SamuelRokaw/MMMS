using System.Collections;
using UnityEngine;

public class BossEnemyAttack : AttackPattern
{
    public float chargeSpeed= 5;
    public float chargeTime= 3f;
    public float stunTime = 1f;
    public EnemyMovement em;
    public override void attack()
    {
        base.attack();
        StartCoroutine(boostTime());

    }

    private IEnumerator boostTime()
    {
        em.spdMod = chargeSpeed;
        yield return new WaitForSeconds(chargeTime);
        em.spdMod = 0f;
        em.rotspdMod = 0f;
        yield return new WaitForSeconds(stunTime);
        em.spdMod = 1f;
        em.rotspdMod = 1f;
    }
}
