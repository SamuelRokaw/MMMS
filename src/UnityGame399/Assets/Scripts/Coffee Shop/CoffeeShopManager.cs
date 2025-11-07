using System.Collections.Generic;
using UnityEngine;

public class CoffeeShopManager : MonoBehaviour
{
    public static CoffeeShopManager Instance;
    
    public int decafCoffeeGrounds = 0;
    public int caffeinatedCoffeeGrounds = 0;
    public int decafBrews = 0;
    public int caffeinatedBrews = 0;
    public List<Coffee> coffees = new List<Coffee>();
    
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

    public void CompleteGrinding(BeanType type)
    {
        if (type == BeanType.Caffeinated)
        {
            caffeinatedCoffeeGrounds++;
        }
        else if (type == BeanType.Decaf)
        {
            decafCoffeeGrounds++;
        }
    }

    public void CompleteBrewing(BeanType type)
    {
        if (type == BeanType.Caffeinated)
        {
            caffeinatedBrews++;
        }
        else if (type == BeanType.Decaf)
        {
            decafBrews++;
        }
    }

    public void CompleteTopping(Coffee coffee, BeanType brewType)
    {
        if (brewType == BeanType.Caffeinated)
        {
            caffeinatedBrews--;
        }
        else if (brewType == BeanType.Decaf)
        {
            decafBrews--;
        }
        
        coffees.Add(coffee);
    }
}