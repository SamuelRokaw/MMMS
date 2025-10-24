using UnityEngine;
using System;
using System.Collections;

public class BossAquarium : Interactable
{
    public GameObject combatEncounterPrefab;
    public static Action bossDead;
    public Stats stats;
    public void OnEnable()
    {
        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        bossDead += disableBattle;
    }

    private void Unsubscribe()
    {
        bossDead -= disableBattle;
    }
    

    public override void Interact()
    {
        if (!used)
        {
            Logger.Instance.Info("Boss interact");
            CombatLoader.Instance.LoadCombat(combatEncounterPrefab);
        }
    }

    private void disableBattle()
    {
        used = true;
        stats.HasDashSkill = true;
    }




}
