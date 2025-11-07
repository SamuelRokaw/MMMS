using UnityEngine;
using System.Collections.Generic;


public class CoffeeBrewerTimeTracker : MonoBehaviour
{
    public static CoffeeBrewerTimeTracker Instance;
    
    public float brewDuration = 30f;
    
    private Dictionary<int, BrewerState> brewerStates = new Dictionary<int, BrewerState>();

    private class BrewerState
    {
        public BrewerStatus status = BrewerStatus.Idle;
        public float startTime;
        public BeanType beanType;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        foreach (var kvp in brewerStates)
        {
            if (kvp.Value.status == BrewerStatus.Brewing)
            {
                float elapsed = Time.time - kvp.Value.startTime;
                if (elapsed >= brewDuration)
                {
                    kvp.Value.status = BrewerStatus.Complete;
                    Debug.Log($"Brewer {kvp.Key} finished brewing!");
                }
            }
        }
    }

    private bool checkIfCanBrew(BeanType type)
    {
        if (type == BeanType.Caffeinated && CoffeeShopManager.Instance.caffeinatedCoffeeGrounds <= 0)
        {
            return false;
        }
        if (type == BeanType.Decaf && CoffeeShopManager.Instance.decafCoffeeGrounds <= 0)
        {
            return false;
        }

        return true;
    }
    
    private void DecrementBean(BeanType type) 
    {
        if (type == BeanType.Caffeinated)
        {
            CoffeeShopManager.Instance.caffeinatedCoffeeGrounds--;
        }
        else if (type == BeanType.Decaf)
        {
            CoffeeShopManager.Instance.decafCoffeeGrounds--;
        }
    }

    public bool StartBrewing(int brewerID, BeanType type)
    {
        if (GetBrewerStatus(brewerID) == BrewerStatus.Brewing)
        {
            return false;
        }

        if (!checkIfCanBrew(type))
        {
            return false;
        }
        
        DecrementBean(type);
        
        
        if (!brewerStates.ContainsKey(brewerID))
        {
            brewerStates[brewerID] = new BrewerState();
        }

        brewerStates[brewerID].status = BrewerStatus.Brewing;
        brewerStates[brewerID].startTime = Time.time;
        brewerStates[brewerID].beanType = type;

        return true;
    }

    public BeanType CollectBrew(int brewerID)
    {
        if (!brewerStates.ContainsKey(brewerID) || brewerStates[brewerID].status != BrewerStatus.Complete)
        {
            return BeanType.Decaf;
        }

        BeanType type = brewerStates[brewerID].beanType;
        
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.CompleteBrewing(type);
        }
        
        brewerStates[brewerID].status = BrewerStatus.Idle;

        return type;
    }

    public BrewerStatus GetBrewerStatus(int brewerID)
    {
        if (!brewerStates.ContainsKey(brewerID))
        {
            return BrewerStatus.Idle;
        }
        return brewerStates[brewerID].status;
    }
    
    public bool IsBrewerActive(int brewerID)
    {
        return GetBrewerStatus(brewerID) == BrewerStatus.Brewing;
    }

    public bool IsBrewerComplete(int brewerID)
    {
        return GetBrewerStatus(brewerID) == BrewerStatus.Complete;
    }

    public float GetBrewerTimeRemaining(int brewerID)
    {
        if (GetBrewerStatus(brewerID) != BrewerStatus.Brewing)
        {
            return 0f;
        }

        float elapsed = Time.time - brewerStates[brewerID].startTime;
        return Mathf.Max(0f, brewDuration - elapsed);
    }
}