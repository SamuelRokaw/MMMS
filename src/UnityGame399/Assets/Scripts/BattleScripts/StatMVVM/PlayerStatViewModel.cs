using System;
using UnityEngine;

public class PlayerStatViewModel : MonoBehaviour
{
    
    [SerializeField] private PlayerStatModel model;

    public event Action<int, int> OnHeartsChanged;
    public event Action<float, float> OnAirChanged;
    public event Action<int, int> OnSkillPointsChanged;

    private void OnEnable()
    {
        model.OnHealthChange += HandleHealthChanged;
        model.OnAirChange += HandleAirChanged;
        model.OnSkillPointsChanged += HandleSkillChanged;
    }

    private void OnDisable()
    {
        model.OnHealthChange -= HandleHealthChanged;
        model.OnAirChange -= HandleAirChanged;
        model.OnSkillPointsChanged -= HandleSkillChanged;
    }

    private void Start()
    {
        HandleHealthChanged(model.CurrentHealth, model.MaxHealth);
        HandleAirChanged(model.CurrentAir, model.MaxAir);
        HandleSkillChanged(model.CurrentSkillPoints, model.MaxSkillPoints);
    }

    private void HandleHealthChanged(int current, int max)
        => OnHeartsChanged?.Invoke(current, max);

    private void HandleAirChanged(float current, float max)
        => OnAirChanged?.Invoke(current, max);

    private void HandleSkillChanged(int current, int max)
        => OnSkillPointsChanged?.Invoke(current, max);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
