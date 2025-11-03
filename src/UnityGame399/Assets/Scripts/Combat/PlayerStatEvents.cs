using UnityEngine;
using System;
using System.Collections;
using PlayerStuff;

public class PlayerStatEvents :MonoBehaviour
{
    public Stats stats;
    
    
    //events
    public static Action<int> PlayerTakesDamage;
    public static Action Die;
    public static Action<int> DecreaseOxygen;
    public static Action<int> DecreaseSP;
    public static Action<int> IncreaseSP;
    
    
    
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
        UpgradeItem.upgradeStat += OnItemInteracted;
        PlayerTakesDamage += DamageTaken;
        stats.OnDie += dies; //these events are in the base PlayerStats so they have to be subscribed like this
        stats.OnTakeDamage += DamageTaken2; //these events are in the base PlayerStats so they have to be subscribed like this
        DecreaseSP += useSP;
        IncreaseSP += gainSP;
    }

    private void Unsubscribe()
    {
        UpgradeItem.upgradeStat -= OnItemInteracted;
        PlayerTakesDamage -= DamageTaken;
        stats.OnTakeDamage -= DamageTaken2; //these events are in the base PlayerStats so they have to be unsubscribed like this
        stats.OnDie -= dies;//these events are in the base PlayerStats so they have to be unsubscribed like this
        DecreaseSP -= useSP;
        IncreaseSP -= gainSP;
    }

    private void OnItemInteracted(string upgradeName)
    {
        if (upgradeName.ToLower() == "attack")
        {
            Logger.Instance.Info("Player Attack increases");
            stats.IncreaseAttack(1);
        }
        else if (upgradeName.ToLower() == "health")
        {
            Logger.Instance.Info("Player health increases");
            stats.IncreaseMaxHealth(1);
        }
        else if (upgradeName.ToLower() == "spear")
        {
            Logger.Instance.Info("Player unlocked spear skill");
            stats.HasSpearSkill = true;
        }
    }

    private void DamageTaken(int damage)
    {
        Logger.Instance.Info($"Player Takes {damage} Damage");
        stats.TakeDamage(damage);
    }
    

    private void dies()
    {
        Logger.Instance.Info("Played died");
        Die.Invoke();
    }

    private void DamageTaken2(int damage) //because of wrapper class player cant drown properly unless we do this
    {
        Logger.Instance.Info($"Player Takes {damage} Damage");
        PlayerTakesDamage(damage);
    }

    private void useSP(int amount)
    {
        Logger.Instance.Info($"Player used {amount} SP");
        stats.UseSP(amount);
    }

    private void gainSP(int amount)
    {
        Logger.Instance.Info($"Player gains {amount} SP");
        stats.GainSP(amount);
    }
}
