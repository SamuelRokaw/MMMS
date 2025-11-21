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
    public int StatPoints => stats.StatPoints;
    public int MaxHealth => stats.MaxHealth;
    public int CurrentHealth => stats.CurrentHealth;
    public int MaxSP => stats.MaxSP;
    public int CurrentSP => stats.CurrentSP;
    public int Luck => stats.Luck;
    public int currentGold => stats.CurrentGold;
    public int currentCafBean => stats.CurrentCafBean;
    public int currentDecafBean => stats.CurrentDecafBean;
    public int currentCarCreamer => stats.CurrentCarCreamer;
    public int currentMilkCreamer => stats.CurrentMilkCreamer;
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
    public void IncreaseMaxSP(int amount) => stats.IncreaseMaxSP(amount);
    public void IncreaseLuck(int amount) => stats.IncreaseLuck(amount);
    public void ChangeGold(int amount) => stats.ChangeGold(amount);
    public void ChangeCafBean(int amount) => stats.ChangeCafBean(amount);
    public void ChangeDecafBean(int amount) => stats.ChangeDecafBean(amount);
    public void ChangeCarCreamer(int amount) => stats.ChangeCarCreamer(amount);
    public void ChangeMilkCreamer(int amount) => stats.ChangeMilkCreamer(amount);
    public void ChangeStatPoints(int amount) => stats.ChangeStatPoints(amount);
    public void Reset() => stats.Reset();
    
    public event Action OnDie;
    public event Action<int> OnTakeDamage;
    public event Action OnLevelUp;

    [SerializeField] private bool loadDataOnAwake; //set to false in start screen to allow deleting of old save, set true in world scene to allow loading of save
    void Awake()
    {
        stats = new PlayerStats();
        // Forward PlayerStats events to Stats events
        stats.OnDie += () => OnDie?.Invoke();
        stats.OnTakeDamage += damage => OnTakeDamage?.Invoke(damage);
        stats.OnLevelUp += () => OnLevelUp?.Invoke();
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
            luck = stats.Luck,
            currentGold = stats.CurrentGold,
            currentCafBean = stats.CurrentCafBean,
            currentDecafBean = stats.CurrentDecafBean,
            currentCarCreamer = stats.CurrentCarCreamer,
            currentMilkCreamer = stats.CurrentMilkCreamer,
            attackPower = stats.AttackPower,
            hasDashSkill = stats.HasDashSkill,
            hasSpearSkill = stats.HasSpearSkill,
            hasThreeSkill = stats.hasThreeSkill,
            hasFourSkill = stats.hasFourSkill,
            hasFiveSkill = stats.hasFiveSkill,
            hasSixSkill = stats.hasSixSkill,
            skillOne = stats.SkillOne,
            skillTwo = stats.SkillTwo,
            statPoints = stats.StatPoints,
        };
    }
    
    public void SaveToPlayerPrefs()
    {
        if(StateManager.Instance.currentShopState == ShopStates.Transition)
        {
            StatsData data = Save();
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("PlayerStats", json);
            PlayerPrefs.Save();
        }
        else
        {
            Logger.Instance.Info("Player Not Saved because it is not transition state");
        }
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
        stats.Luck = data.luck;
        stats.CurrentGold = data.currentGold;
        stats.CurrentCafBean = data.currentCafBean;
        stats.CurrentDecafBean = data.currentDecafBean;
        stats.CurrentCarCreamer = data.currentCarCreamer;
        stats.CurrentMilkCreamer = data.currentMilkCreamer;
        stats.AttackPower = data.attackPower;
        stats.HasDashSkill = data.hasDashSkill;
        stats.HasSpearSkill = data.hasSpearSkill;
        stats.hasThreeSkill = data.hasThreeSkill;
        stats.hasFourSkill = data.hasFourSkill;
        stats.hasFiveSkill = data.hasFiveSkill;
        stats.hasSixSkill = data.hasSixSkill;
        stats.SkillOne = data.skillOne;
        stats.SkillTwo = data.skillTwo;
        stats.StatPoints = data.statPoints;
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
        stats.Luck = 0;
        stats.StatPoints = 0;
        stats.CurrentGold = 100;
        stats.CurrentCafBean = 50;
        stats.CurrentDecafBean = 50;
        stats.CurrentCarCreamer = 0;
        stats.CurrentMilkCreamer = 50;
        stats.AttackPower = 1;
        stats.HasDashSkill = true;
        stats.HasSpearSkill = true;
        stats.hasThreeSkill = false;
        stats.hasFourSkill = false;
        stats.hasFiveSkill = false;
        stats.hasSixSkill = false;
        stats.SkillOne = SkillTypes.Dash;
        stats.SkillTwo = SkillTypes.Spear;
        SaveToPlayerPrefs();
    }
}