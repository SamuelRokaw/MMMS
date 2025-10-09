using System;
using UnityEngine;

public class PlayerStatModel : MonoBehaviour
{
    public Stats stats;
    // health
    public int MaxHealth = 3;
    public int CurrentHealth { get; private set; }
    
    // air
    public float MaxAir = 100f;
    public float CurrentAir = 100f;

    [SerializeField] private float airDepletionRate = 5f;
    private bool depleteAir = true;
    
    // SP
    public int MaxSkillPoints = 5;
    public int CurrentSkillPoints { get; private set; }
    
    public event Action<int, int> OnHealthChange;
    public event Action<float, float> OnAirChange;
    public event Action<int, int> OnSkillPointsChanged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
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
        DepleteAir();
    }

    private void DepleteAir()
    {
        if (!depleteAir || CurrentAir <= 0) return;

        CurrentAir -= airDepletionRate * Time.deltaTime;
        CurrentAir = Mathf.Clamp(CurrentAir, 0, MaxAir);

        OnAirChange?.Invoke(CurrentAir, MaxAir);
        
        if (CurrentAir <= 0)
        {
            depleteAir = false;
            OnAirEmptied();
        }
    }

    private void OnAirEmptied()
    {
        PlayerStatEvents.PlayerTakesDamage.Invoke(1); //will still instakill because void Update
    }
}