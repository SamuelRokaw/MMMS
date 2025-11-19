using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class CoffeeToppingBrewListManager : MonoBehaviour
{
    [System.Serializable]
    public class BrewButton
    {
        public Button button;
        public TextMeshProUGUI countText;
        public Sprite normalSprite;
        public Sprite pressedSprite;
        public Image image;
        public BeanType beanType;
    }
    
    public List<BrewButton> brewsButtons = new List<BrewButton>();
    private bool isLocked = false;
    
    private int selectedIndex = -1;

    private void OnEnable()
    {
        UpdateBrewDisplay();
    }

    private void Start()
    {
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.OnCoffeeAdded.AddListener(UpdateBrewDisplay);
        }
        for (int i = 0; i < brewsButtons.Count; i++)
        {
            BrewButton bb = brewsButtons[i];
            
            bb.image = bb.button.GetComponent<Image>();
            bb.image.sprite = bb.normalSprite;
            
            int index = i;
            bb.button.onClick.AddListener(() => OnButtonClicked(index));
        }
        
        UpdateBrewDisplay();
    }

    public void UpdateBrewDisplay()
    {
        if (CoffeeShopManager.Instance == null) return;

        foreach (BrewButton bb in brewsButtons)
        {
            int count = 0;
            
            foreach (Coffee coffee in CoffeeShopManager.Instance.coffees)
            {
                if (coffee.BeanType == bb.beanType && coffee.CreamerType == CreamerType.None)
                {
                    count++;
                }
            }
        
            bb.countText.text = $"{bb.beanType.ToString()}: ({count})";
        }
    }
    
    public int GetFirstPlainCoffeeIndex()
    {
        if (!HasSelection() || CoffeeShopManager.Instance == null)
        {
            return -1;
        }

        BeanType selectedBeanType = GetSelectedBeanType();
        
        for (int i = 0; i < CoffeeShopManager.Instance.coffees.Count; i++)
        {
            Coffee coffee = CoffeeShopManager.Instance.coffees[i];
            if (coffee.BeanType == selectedBeanType && coffee.CreamerType == CreamerType.None)
            {
                return i;
            }
        }

        return -1;
    }

    private void OnButtonClicked(int index)
    {
        if (isLocked)
        {
            Logger.Instance.Info("Can't change brew while pouring!");
            return;
        }
        BrewButton bb = brewsButtons[index];
        int plainCoffeeCount = 0;
        foreach (Coffee coffee in CoffeeShopManager.Instance.coffees)
        {
            if (coffee.BeanType == bb.beanType && coffee.CreamerType == CreamerType.None)
            {
                plainCoffeeCount++;
            }
        }

        if (plainCoffeeCount <= 0)
        {
            Logger.Instance.Info($"No plain {bb.beanType} coffees available!");
            return;
        }
    
        if (selectedIndex == index)
        {
            brewsButtons[index].image.sprite = brewsButtons[index].normalSprite;
            selectedIndex = -1;
            Logger.Instance.Info("Deselected brew");
            return;
        }
    
        if (selectedIndex >= 0 && selectedIndex < brewsButtons.Count)
        {
            brewsButtons[selectedIndex].image.sprite = brewsButtons[selectedIndex].normalSprite;
        }
    
        selectedIndex = index;
        brewsButtons[index].image.sprite = brewsButtons[index].pressedSprite;
        Logger.Instance.Info($"Selected: {bb.beanType} brew");
    }

    public int GetSelectedIndex() => selectedIndex;
    public bool HasSelection() => selectedIndex >= 0;
    
    public BeanType GetSelectedBeanType()
    {
        if (selectedIndex >= 0 && selectedIndex < brewsButtons.Count)
        {
            return brewsButtons[selectedIndex].beanType;
        }
        return BeanType.Decaf;
    }

    public void DeselectAll()
    {
        if (selectedIndex >= 0 && selectedIndex < brewsButtons.Count)
        {
            brewsButtons[selectedIndex].image.sprite = brewsButtons[selectedIndex].normalSprite;
        }
        selectedIndex = -1;
    }
    
    public void LockSelection()
    {
        isLocked = true;
        Debug.Log("Brew selection locked");
    }

    public void UnlockSelection()
    {
        isLocked = false;
        Debug.Log("Brew selection unlocked");
    }
    
    private void OnDestroy()
    {
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.OnCoffeeAdded.RemoveListener(UpdateBrewDisplay);
        }
    }

    public bool IsLocked() => isLocked;
}