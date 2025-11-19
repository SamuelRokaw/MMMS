using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;

public class CoffeeInventoryManager : MonoBehaviour
{
    public Transform coffeeGridContainer;
    public GameObject coffeePrefab;
    public InventorySlidePanel slidePanel;
    
    public static UnityEvent<int> OnCoffeeItemClicked = new UnityEvent<int>();

    private int selectedCoffeeIndex = -1; 
    private List<CoffeeItem> coffeeGridItems = new List<CoffeeItem>();

    private void Start()
    {
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.OnCoffeeAdded.AddListener(RefreshInventory);
        }
        
        RefreshInventory();
    }

    private void OnDestroy()
    {
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.OnCoffeeAdded.RemoveListener(RefreshInventory);
        }
    }

    public void RefreshInventory()
    {
        foreach (var item in coffeeGridItems)
        {
            if (item != null && item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
        coffeeGridItems.Clear();

        if (CoffeeShopManager.Instance == null) return;
        
        for (int i = 0; i < CoffeeShopManager.Instance.coffees.Count; i++)
        {
            Coffee coffee = CoffeeShopManager.Instance.coffees[i];
            GameObject coffeeObj = Instantiate(coffeePrefab, coffeeGridContainer);
            
            CoffeeItem gridItem = coffeeObj.GetComponent<CoffeeItem>();
            if (gridItem == null)
            {
                gridItem = coffeeObj.AddComponent<CoffeeItem>();
            }

            int index = i;
            gridItem.Initialize(coffee, index, this);
            coffeeGridItems.Add(gridItem);
        }
    }

    public void SelectCoffee(int index)
    {
        if (selectedCoffeeIndex >= 0 && selectedCoffeeIndex < coffeeGridItems.Count)
        {
            coffeeGridItems[selectedCoffeeIndex].SetSelected(false);
        }
        
        if (selectedCoffeeIndex == index)
        {
            selectedCoffeeIndex = -1;
            Debug.Log("Coffee deselected");
        }
        else
        {
            selectedCoffeeIndex = index;
            coffeeGridItems[index].SetSelected(true);
            Debug.Log($"Selected coffee #{index}: {CoffeeShopManager.Instance.coffees[index].BeanType}");
        }
        
        OnCoffeeItemClicked?.Invoke(index);
    }

    public int GetSelectedCoffeeIndex() => selectedCoffeeIndex;
    public bool HasSelection() => selectedCoffeeIndex >= 0;

    public Coffee GetSelectedCoffee()
    {
        if (selectedCoffeeIndex >= 0 && selectedCoffeeIndex < CoffeeShopManager.Instance.coffees.Count)
        {
            return CoffeeShopManager.Instance.coffees[selectedCoffeeIndex];
        }
        return null;
    }
}