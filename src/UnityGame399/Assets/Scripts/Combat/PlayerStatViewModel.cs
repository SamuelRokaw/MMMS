using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatViewModel : MonoBehaviour
{
    public Stats stats;
    
    //[SerializeField] private PlayerStatModel model;

    public static event Action<int, int> OnHeartsChanged;
    public static event Action<float, float> OnAirChanged;
    public static event Action<int, int> OnSkillPointsChanged;

    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
    }
    private void OnEnable()
    {
        PlayerStatEvents.PlayerTakesDamage += HandleHealthChanged;
        PlayerStatEvents.DecreaseOxygen += HandleAirChanged;
        PlayerStatEvents.DecreaseSP += HandleSkillChanged;
        PlayerStatEvents.IncreaseSP += HandleSkillChanged;
    }

    private void OnDisable()
    {
        PlayerStatEvents.PlayerTakesDamage -= HandleHealthChanged;
        PlayerStatEvents.DecreaseOxygen -= HandleAirChanged;
        PlayerStatEvents.DecreaseSP -= HandleSkillChanged;
        PlayerStatEvents.IncreaseSP -= HandleSkillChanged;
    }

    
    private IEnumerator Start()
    {
        yield return null;
        HandleHealthChanged(0);
        HandleAirChanged(0);
        HandleSkillChanged(0);
    }
    

    private void HandleHealthChanged(int amount)
        => OnHeartsChanged?.Invoke(stats.CurrentHealth, stats.MaxHealth);

    private void HandleAirChanged(int amount)
        => OnAirChanged?.Invoke(stats.CurrentOxygen, stats.MaxOxygen);

    private void HandleSkillChanged(int amount)
        => OnSkillPointsChanged?.Invoke(stats.CurrentSP, stats.MaxSP);
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        
    }
}
