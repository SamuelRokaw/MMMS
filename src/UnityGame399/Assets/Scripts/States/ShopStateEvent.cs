using UnityEngine;
using System;

public class ShopStateEvent : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private CoffeeShop coffeeShop;
    public static Action TransitionState;
    public static Action DayState;
    public static Action NightState;
    [SerializeField] private GameObject dayTimeUI;
    
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
        TransitionState += SavesGame;
        TransitionState += TurnOffDayTimeUI;
        DayState += TurnOnDayTimeUI;

    }

    private void Unsubscribe()
    {
        TransitionState -= SavesGame;
        TransitionState -= TurnOffDayTimeUI;
        DayState -= TurnOnDayTimeUI;
    }

    private void SavesGame()
    {
        stats.SaveToPlayerPrefs();
        coffeeShop.SaveToPlayerPrefs();
    }

    private void TurnOnDayTimeUI()
    {
        dayTimeUI.SetActive(true);
    }
    private void TurnOffDayTimeUI()
    {
        dayTimeUI.SetActive(false);
    }
}
