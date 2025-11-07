using UnityEngine;
using System.Collections;
using System;

public class WaveDefense : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;
    
    public static Action<int> Takedamage;
    public static Action<int> Heal;
    public static Action Destroyed;
    
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
        Takedamage += ChangeHealth;
        Heal += ChangeHealth;
    }

    private void Unsubscribe()
    {
        Takedamage -= ChangeHealth;
        Heal -= ChangeHealth;
        
    }

    private void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Destroyed.Invoke();
        }
    }
}
