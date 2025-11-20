using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PlayerStuff;
using Random = UnityEngine.Random;

public class Cycle : MonoBehaviour
{
    [SerializeField] private float cycleDuration = 15f; // put amount of minutes here
    public int maxCustomers = 5;
    public int chanceToSpawnCustomer = 10; //chance to spawn a customer every second, default is 10%
    public AdUpgrade adUpgrade;
    public CustomerManager customerManager;


    public void Start()
    {
        if (StateManager.Instance.currentShopState == ShopStates.DayTime)
        {
            StartCycle();
        }
        else if (StateManager.Instance.currentShopState == ShopStates.NightTime)
        {
            StartCycle();
        }
    }
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
        ShopStateEvent.DayState += StartCycle;
        ShopStateEvent.NightState += StartCycle;
    }

    private void Unsubscribe()
    {
        ShopStateEvent.DayState -= StartCycle;
        ShopStateEvent.NightState -= StartCycle;
    }

    public void StartCycle()
    {
        StartCoroutine(TimeCycle(cycleDuration));
    }


    private void RollToSpawnCustomer()
    {
        int odds = chanceToSpawnCustomer;
        if (adUpgrade.unlocked)
        {
            odds += adUpgrade.amountToIncreaseBy;
        }

        if (odds >= Random.Range(1, 101))
        {
            SpawnCustomer();
        }
    }
    private void SpawnCustomer()
    {
        if ((customerManager.GetWaitingCustomersCount() + customerManager.GetCustomersWithOrders()) < maxCustomers)
        {
            Logger.Instance.Info("Customer will be spawned");
            customerManager.SpawnCustomer();
        }
    }

    private IEnumerator TimeCycle(float length)
    {
        int i = 0;
        while (i < length * 60)
        {
            yield return new WaitForSeconds(1f);
            i++;
            if (StateManager.Instance.currentShopState == ShopStates.DayTime)
            {
                RollToSpawnCustomer();
            }
        }

        if (StateManager.Instance.currentShopState == ShopStates.DayTime)
        {
            if ((customerManager.GetWaitingCustomersCount() + customerManager.GetCustomersWithOrders()) > 0)
            {
                StateManager.Instance.SwitchShopToOverTime();
            }
            else
            {
                StateManager.Instance.SwitchShopToTransition();
            }
        }
        else
        {
            if (StateManager.Instance.currentGameState == GameStates.Combat)
            {
                StateManager.Instance.SwitchShopToOverTime();
            }
            else
            {
                StateManager.Instance.SwitchShopToTransition();
            }
        }
    }
}
