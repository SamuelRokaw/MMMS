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
        PlayerStats.OnDie += dies;
        PlayerStats.OnTakeDamage += DamageTaken2;
        DecreaseOxygen += Drowning;
        DecreaseSP += useSP;
        IncreaseSP += gainSP;
    }

    private void Unsubscribe()
    {
        UpgradeItem.upgradeStat -= OnItemInteracted;
        PlayerTakesDamage -= DamageTaken;
        PlayerStats.OnTakeDamage -= DamageTaken2;
        PlayerStats.OnDie -= dies;
        DecreaseOxygen -= Drowning;
        DecreaseSP -= useSP;
        IncreaseSP -= gainSP;
    }

    private void OnItemInteracted(string upgradeName)
    {
        if (upgradeName.ToLower() == "attack")
        {
            Debug.Log("Player Attack increases");
            stats.IncreaseAttack(1);
        }
        else if (upgradeName.ToLower() == "health")
        {
            Debug.Log("Player health increases");
            stats.IncreaseMaxHealth(1);
        }
    }

    private void DamageTaken(int damage)
    {
        Debug.Log("Player Takes Damage");
        stats.TakeDamage(damage);
    }

    private void Drowning(int amount)
    {
        stats.DecreaseOxygen(amount);
    }

    private void dies()
    {
        Die.Invoke();
    }

    private void DamageTaken2(int damage) //because of wrapper class player cant drown properly unless we do this
    {
        PlayerTakesDamage(damage);
    }

    private void useSP(int amount)
    {
        stats.UseSP(amount);
    }

    private void gainSP(int amount)
    {
        stats.GainSP(amount);
    }
}
