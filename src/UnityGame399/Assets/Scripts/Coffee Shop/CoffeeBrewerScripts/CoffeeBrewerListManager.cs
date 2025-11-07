using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class CoffeeBrewerListManager : MonoBehaviour
{
    [System.Serializable]
    public class GroundsButton
    {
        public Button button;
        public TextMeshProUGUI countText;
        public Sprite normalSprite;
        public Sprite pressedSprite;
        public Image image;
        public BeanType beanType;
    }
    
    public List<GroundsButton> groundsButtons = new List<GroundsButton>();

    private int selectedIndex = -1;

    private void OnEnable()
    {
        UpdateGroundsDisplay();
    }

    private void Start()
    {
        for (int i = 0; i < groundsButtons.Count; i++)
        {
            GroundsButton gb = groundsButtons[i];
            
            gb.image = gb.button.GetComponent<Image>();
            gb.image.sprite = gb.normalSprite;
            
            int index = i;
            gb.button.onClick.AddListener(() => OnButtonClicked(index));
        }
        
        UpdateGroundsDisplay();
    }

    public void UpdateGroundsDisplay()
    {
        if (CoffeeShopManager.Instance == null) return;

        foreach (GroundsButton gb in groundsButtons)
        {
            int count = 0;
            
            if (gb.beanType == BeanType.Decaf)
            {
                count = CoffeeShopManager.Instance.decafCoffeeGrounds;
            }
            else if (gb.beanType == BeanType.Caffeinated)
            {
                count = CoffeeShopManager.Instance.caffeinatedCoffeeGrounds;
            }
            gb.countText.text = $"{gb.beanType.ToString()}: ({count})";
        }
    }

    private void OnButtonClicked(int index)
    {
        GroundsButton gb = groundsButtons[index];
        int availableGrounds = gb.beanType == BeanType.Decaf 
            ? CoffeeShopManager.Instance.decafCoffeeGrounds 
            : CoffeeShopManager.Instance.caffeinatedCoffeeGrounds;

        if (availableGrounds <= 0)
        {
            Logger.Instance.Info($"No {gb.beanType} grounds available!");
            return;
        }
        
        if (selectedIndex == index)
        {
            groundsButtons[index].image.sprite = groundsButtons[index].normalSprite;
            selectedIndex = -1;
            Logger.Instance.Info("Deselected grounds");
            return;
        }
        
        if (selectedIndex >= 0 && selectedIndex < groundsButtons.Count)
        {
            groundsButtons[selectedIndex].image.sprite = groundsButtons[selectedIndex].normalSprite;
        }
        
        selectedIndex = index;
        groundsButtons[index].image.sprite = groundsButtons[index].pressedSprite;
        Logger.Instance.Info($"Selected: {gb.beanType} grounds");
    }

    public int GetSelectedIndex() => selectedIndex;
    public bool HasSelection() => selectedIndex >= 0;
    
    public BeanType GetSelectedBeanType()
    {
        if (selectedIndex >= 0 && selectedIndex < groundsButtons.Count)
        {
            return groundsButtons[selectedIndex].beanType;
        }
        return BeanType.Decaf;
    }

    public void DeselectAll()
    {
        if (selectedIndex >= 0 && selectedIndex < groundsButtons.Count)
        {
            groundsButtons[selectedIndex].image.sprite = groundsButtons[selectedIndex].normalSprite;
        }
        selectedIndex = -1;
    }
}