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
    public int MaxMana => maxSP;
    public int CurrentMana => currentSP;
    public int MaxStamina => maxOxygen;
    public int CurrentStamina => currentOxygen;
    public int AttackPower => attack;
    

    void Start()
    {
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
            //Player.die();
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

    public void Reset()
    {
        currentHealth = maxHealth;
        currentSP = maxSP;
        currentOxygen = maxOxygen;
    }
}
