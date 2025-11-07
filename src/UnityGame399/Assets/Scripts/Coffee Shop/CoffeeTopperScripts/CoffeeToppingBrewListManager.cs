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
            
            if (bb.beanType == BeanType.Decaf)
            {
                count = CoffeeShopManager.Instance.decafBrews;
            }
            else if (bb.beanType == BeanType.Caffeinated)
            {
                count = CoffeeShopManager.Instance.caffeinatedBrews;
            }
            bb.countText.text = $"{bb.beanType.ToString()}: ({count})";
        }
    }

    private void OnButtonClicked(int index)
    {
        if (isLocked)
        {
            Logger.Instance.Info("Can't change brew while pouring!");
            return;
        }
        BrewButton bb = brewsButtons[index];
        int availableGrounds = bb.beanType == BeanType.Decaf 
            ? CoffeeShopManager.Instance.decafBrews
            : CoffeeShopManager.Instance.caffeinatedBrews;

        if (availableGrounds <= 0)
        {
            Logger.Instance.Info($"No {bb.beanType} brews available!");
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

    public bool IsLocked() => isLocked;
}