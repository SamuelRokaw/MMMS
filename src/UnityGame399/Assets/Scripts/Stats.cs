using UnityEngine;
using System;
using PlayerStuff; // your namespace for Stats

public class Stats : MonoBehaviour
{
    public PlayerStats stats;
    
    public int Level => stats.Level;
    public int Experience => stats.Experience;
    public int ExperienceToNextLevel => stats.ExperienceToNextLevel;
    public int MaxHealth => stats.MaxHealth;
    public int CurrentHealth => stats.CurrentHealth;
    public int MaxSP => stats.MaxSP;
    public int CurrentSP => stats.CurrentSP;
    public int MaxOxygen => stats.MaxOxygen;
    public int CurrentOxygen => stats.CurrentOxygen;
    public int AttackPower => stats.AttackPower;
    public bool HasDashSkill => stats.HasDashSkill;
    public bool HasSpearSkill => stats.HasSpearSkill;
    public bool HasThreeSkill => stats.hasThreeSkill;
    public bool HasFourSkill => stats.hasFourSkill;
    public bool HasFiveSkill => stats.hasFiveSkill;
    public bool HasSixSkill => stats.hasSixSkill;
    
    public void TakeDamage(int amount) => stats.TakeDamage(amount);
    public void Heal(int amount) => stats.Heal(amount);
    public void UseSP(int amount) => stats.UseSP(amount);
    public void GainSP(int amount) => stats.GainSP(amount);
    public void GainExperience(int amount) => stats.GainExperience(amount);
    public void IncreaseMaxHealth(int amount) => stats.IncreaseMaxHealth(amount);
    public void IncreaseAttack(int amount) => stats.IncreaseAttack(amount);
    public void DecreaseOxygen(int amount) => stats.DecreaseOxygen(amount);
    public void Reset() => stats.Reset();

    void Awake()
    {
        stats = new PlayerStats();
        LoadFromPlayerPrefs();
    }

    // Save the current stats to a serializable object
    public StatsData Save()
    {
        return new StatsData
        {
            level = stats.Level,
            experience = stats.Experience,
            experienceToNextLevel = stats.ExperienceToNextLevel,
            maxHealth = stats.MaxHealth,
            currentHealth = stats.CurrentHealth,
            maxSP = stats.MaxSP,
            currentSP = stats.CurrentSP,
            maxOxygen = stats.MaxOxygen,
            currentOxygen = stats.CurrentOxygen,
            attackPower = stats.AttackPower
        };
    }
    
    public void SaveToPlayerPrefs()
    {
        StatsData data = Save();
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerStats", json);
        PlayerPrefs.Save();
    }

    public void LoadFromPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("PlayerStats")) return;
        string json = PlayerPrefs.GetString("PlayerStats");
        StatsData data = JsonUtility.FromJson<StatsData>(json);
        Load(data);
    }
    
    [ContextMenu("Reset Stats")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("PlayerStats");
        PlayerPrefs.Save();
    }

    // Load stats from a StatsData object
    public void Load(StatsData data)
    {
        stats.Reset(); // Reset to defaults first

        // Manually set the fields
        stats.Level = data.level;
        stats.Experience = data.experience;
        stats.ExperienceToNextLevel = data.experienceToNextLevel;
        stats.MaxHealth = data.maxHealth;
        stats.CurrentHealth = data.currentHealth;
        stats.MaxSP = data.maxSP;
        stats.CurrentSP = data.currentSP;
        stats.MaxOxygen = data.maxOxygen;
        stats.CurrentOxygen = data.currentOxygen;
        stats.AttackPower = data.attackPower;
    }
}