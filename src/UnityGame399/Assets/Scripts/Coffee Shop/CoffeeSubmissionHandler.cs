using UnityEngine;
public class CoffeeSubmissionHandler : MonoBehaviour
{
    public static CoffeeSubmissionHandler Instance;

    private Coffee selectedCoffee = null;
    private int selectedCoffeeInventoryIndex = -1;

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
    
    public void SelectCoffee(int inventoryIndex)
    {
        if (CoffeeShopManager.Instance == null || 
            inventoryIndex < 0 || 
            inventoryIndex >= CoffeeShopManager.Instance.coffees.Count)
        {
            Logger.Instance.Warn("Invalid coffee index selected!");
            DeselectCoffee();
            return;
        }

        selectedCoffee = CoffeeShopManager.Instance.coffees[inventoryIndex];
        selectedCoffeeInventoryIndex = inventoryIndex;
        
        Logger.Instance.Info($"Coffee selected: {selectedCoffee.BeanType} with {selectedCoffee.CreamPercent * 100:F0}% {selectedCoffee.CreamerType}");
    }
    public void DeselectCoffee()
    {
        selectedCoffee = null;
        selectedCoffeeInventoryIndex = -1;
        Logger.Instance.Info("Coffee deselected");
    }
    
    public bool SubmitSelectedCoffee()
    {
        if (selectedCoffee == null || selectedCoffeeInventoryIndex < 0)
        {
            Logger.Instance.Warn("No coffee selected! Please select a coffee first.");
            return false;
        }
        
        if (CoffeeShopManager.Instance == null || 
            selectedCoffeeInventoryIndex >= CoffeeShopManager.Instance.coffees.Count ||
            CoffeeShopManager.Instance.coffees[selectedCoffeeInventoryIndex] != selectedCoffee)
        {
            Logger.Instance.Warn("Selected coffee no longer exists in inventory!");
            DeselectCoffee();
            return false;
        }
        
        CustomerManager customerManager = FindFirstObjectByType<CustomerManager>();
        if (customerManager == null)
        {
            Logger.Instance.Warn("Customer manager not found!");
            return false;
        }
        
        if (customerManager.GetOrdersCount() == 0)
        {
            Logger.Instance.Warn("No customer orders to fulfill!");
            return false;
        }
        
        customerManager.SubmitCoffee(selectedCoffee);
        
        CoffeeShopManager.Instance.coffees.RemoveAt(selectedCoffeeInventoryIndex);
        CoffeeShopManager.Instance.OnCoffeeAdded?.Invoke();
        
        DeselectCoffee();

        Logger.Instance.Info("Coffee submitted successfully!");
        return true;
    }
    
    public Coffee GetSelectedCoffee() => selectedCoffee;
    public bool HasSelectedCoffee() => selectedCoffee != null && selectedCoffeeInventoryIndex >= 0;
    public int GetSelectedInventoryIndex() => selectedCoffeeInventoryIndex;
}