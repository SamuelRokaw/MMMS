using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PlayerStuff;

public class Cycle : MonoBehaviour
{
    [SerializeField] private float cycleDuration = 15f; // put amount of minutes here
    public int maxCustomers = 5;
    public int chanceToSpawnCustomer = 10; //chance to spawn a customer every second, default is 10%
    public AdUpgrade adUpgrade;
    public CustomerManager customerManager;

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
            Logger.Instance.Info("Customer will be spawned");
            SpawnCustomer();
        }
    }
    private void SpawnCustomer()
    {
        if ((customerManager.GetWaitingCustomersCount() + customerManager.GetCustomersWithOrders()) <= maxCustomers)
        {
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
