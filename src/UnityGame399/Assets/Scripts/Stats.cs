using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    [SerializeField] private int level = -1;
    [SerializeField] private int maxLevel = 60;
    [SerializeField] private int experience = -1;
    [SerializeField] private int experienceToNextLevel = -1;
    [SerializeField] private int maxHealth = -1;
    [SerializeField] private int currentHealth = -1;
    [SerializeField] private int maxSP = -1;
    [SerializeField] private int currentSP = -1;
    [SerializeField] private int maxOxygen = -1;
    [SerializeField] private int currentOxygen = -1;
    [SerializeField] private int attack = -1;
    
    public int Level => level;
    public int Experience => experience;
    public int ExperienceToNextLevel => experienceToNextLevel;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public int MaxSP => maxSP;
    public int CurrentSP => currentSP;
    public int MaxOxygen => maxOxygen;
    public int CurrentOxygen => currentOxygen;
    public int AttackPower => attack;
    

    void Start()
    {
        DefaultValues(); //temporary, this will be called in save creation or game initialization later on
        currentHealth = maxHealth;
        currentSP = maxSP;
        currentOxygen = maxOxygen;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerStatEvents.Die.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
    }

    public void UseSP(int amount)
    {
        currentSP = Mathf.Max(0, currentSP - amount);
    }

    public void GainSP(int amount)
    {
        currentSP = Mathf.Min(maxSP, currentSP + amount);
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (level >= maxLevel) return;
        experience = 0;
        level++;
        experienceToNextLevel += 1;
        maxOxygen += 2;

        if (level % 4 == 0)
        {
            maxSP += 1;
        }
    }

    public void increaseMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public void increaseAttack(int amount)
    {
        attack += amount;
    }

    public void decreaseOxygen(int amount)
    {
        currentOxygen -= amount;
        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            PlayerStatEvents.PlayerTakesDamage.Invoke(1);
        }
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        currentSP = maxSP;
        currentOxygen = maxOxygen;
    }

    public void DefaultValues()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 5;
        maxHealth = 3;
        currentHealth = maxHealth;
        maxSP = 5;
        currentSP = maxSP;
        maxOxygen = 120;
        currentOxygen = maxOxygen;
        attack = 1;
    }
}
