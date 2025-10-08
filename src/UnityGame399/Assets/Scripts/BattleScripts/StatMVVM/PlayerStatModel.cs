using System;
using UnityEngine;

public class PlayerStatModel : MonoBehaviour
{
    // health
    public int MaxHealth = 3;
    public int CurrentHealth { get; private set; }
    
    // air
    public float MaxAir = 100f;
    public float CurrentAir { get; private set; }
    
    // SP
    public int MaxSkillPoints = 5;
    public int CurrentSkillPoints { get; private set; }
    
    public event Action<int, int> OnHealthChange;
    public event Action<float, float> OnAirChange;
    public event Action<int, int> OnSkillPointsChanged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentAir = MaxAir;
        CurrentSkillPoints = MaxSkillPoints;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);
        OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
    }

    public void LoseAir(float amount)
    {
        CurrentAir = Mathf.Clamp(CurrentAir - amount, 0f, MaxAir);
        OnAirChange?.Invoke(CurrentAir, MaxAir);
    }

    public void GainSkillPoints(int amount)
    {
        CurrentSkillPoints = Mathf.Clamp(CurrentSkillPoints + amount, 0, MaxSkillPoints);
        OnSkillPointsChanged?.Invoke(CurrentSkillPoints, MaxSkillPoints);
    }

    public void SpendSkillPoint(int amount)
    {
        CurrentSkillPoints = Mathf.Clamp(CurrentSkillPoints - amount, 0, MaxSkillPoints);
        OnSkillPointsChanged?.Invoke(CurrentSkillPoints, MaxSkillPoints);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
