using UnityEngine;
using System;
using System.Collections.Generic;
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
    public bool HasDashSkill
    {
        get => stats.HasDashSkill;
        set => stats.HasDashSkill = value;
    }
    public bool HasSpearSkill
    {
        get => stats.HasSpearSkill;
        set => stats.HasSpearSkill = value;
    }
    public bool HasThreeSkill
    {
        get => stats.hasThreeSkill;
        set => stats.hasThreeSkill = value;
    }
    public bool HasFourSkill
    {
        get => stats.hasFourSkill;
        set => stats.hasFourSkill = value;
    }
    public bool HasFiveSkill
    {
        get => stats.hasFiveSkill;
        set => stats.hasFiveSkill = value;
    }
    public bool HasSixSkill
    {
        get => stats.hasSixSkill;
        set => stats.hasSixSkill = value;
    }

    public SkillTypes SkillOne
    {
        get => stats.SkillOne;
        set => stats.SkillOne = value;
    }
    public SkillTypes SkillTwo{
        get => stats.SkillTwo;
        set => stats.SkillTwo = value;
    }
    public void TakeDamage(int amount) => stats.TakeDamage(amount);
    public void Heal(int amount) => stats.Heal(amount);
    public void UseSP(int amount) => stats.UseSP(amount);
    public void GainSP(int amount) => stats.GainSP(amount);
    public void GainExperience(int amount) => stats.GainExperience(amount);
    public void IncreaseMaxHealth(int amount) => stats.IncreaseMaxHealth(amount);
    public void IncreaseAttack(int amount) => stats.IncreaseAttack(amount);
    public void DecreaseOxygen(int amount) => stats.DecreaseOxygen(amount);
    public void Reset() => stats.Reset();
    
    public event Action OnDie;
    public event Action<int> OnTakeDamage;

    [SerializeField] private bool loadDataOnAwake; //set to false in start screen to allow deleting of old save, set true in world scene to allow loading of save
    void Awake()
    {
        stats = new PlayerStats();
        // Forward PlayerStats events to Stats events
        stats.OnDie += () => OnDie?.Invoke();
        stats.OnTakeDamage += damage => OnTakeDamage?.Invoke(damage);
        if (loadDataOnAwake)
        {
            LoadFromPlayerPrefs();
        }
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
            attackPower = stats.AttackPower,
            hasDashSkill = stats.HasDashSkill,
            hasSpearSkill = stats.HasSpearSkill,
            hasThreeSkill = stats.hasThreeSkill,
            hasFourSkill = stats.hasFourSkill,
            hasFiveSkill = stats.hasFiveSkill,
            hasSixSkill = stats.hasSixSkill,
            skillOne = stats.SkillOne,
            skillTwo = stats.SkillTwo,
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
        newSave();
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
        stats.HasDashSkill = data.hasDashSkill;
        stats.HasSpearSkill = data.hasSpearSkill;
        stats.hasThreeSkill = data.hasThreeSkill;
        stats.hasFourSkill = data.hasFourSkill;
        stats.hasFiveSkill = data.hasFiveSkill;
        stats.hasSixSkill = data.hasSixSkill;
        stats.SkillOne = data.skillOne;
        stats.SkillTwo = data.skillTwo;
    }

    public void newSave()
    {
        stats.Level = 0;
        stats.Experience = 0;
        stats.ExperienceToNextLevel = 5;
        stats.MaxHealth = 3;
        stats.CurrentHealth = 3;
        stats.MaxSP = 5;
        stats.CurrentSP = 5;
        stats.MaxOxygen = 120;
        stats.CurrentOxygen = 120;
        stats.AttackPower = 1;
        stats.HasDashSkill = false;
        stats.HasSpearSkill = false;
        stats.hasThreeSkill = false;
        stats.hasFourSkill = false;
        stats.hasFiveSkill = false;
        stats.hasSixSkill = false;
        stats.SkillOne = SkillTypes.None;
        stats.SkillTwo = SkillTypes.None;
        SaveToPlayerPrefs();
    }
}