using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoffeeItem : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI coffeeNameText;
    public Button selectButton;
    
    public Sprite normalSprite;
    public Sprite selectedSprite;

    private Coffee coffee;
    private int index;
    private CoffeeInventoryManager inventoryUI;
    private CoffeeDetailPopup detailPopup;
    private bool isSelected = false;

    public void Initialize(Coffee coffee, int index, CoffeeInventoryManager inventoryUI)
    {
        this.coffee = coffee;
        this.index = index;
        this.inventoryUI = inventoryUI;

        // Display simple coffee name
        if (coffeeNameText != null)
        {
            coffeeNameText.text = $"Coffee{index + 1}";
        }

        // Setup button
        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnClicked);
        }
        else
        {
            // If no button assigned, add one to this object
            selectButton = gameObject.GetComponent<Button>();
            if (selectButton == null)
            {
                selectButton = gameObject.AddComponent<Button>();
            }
            selectButton.onClick.AddListener(OnClicked);
        }

        SetSelected(false);
    }

    private void OnClicked()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SelectCoffee(index);
        }
        
        CoffeeDetailPopup popup = FindFirstObjectByType<CoffeeDetailPopup>();
        if (popup != null)
        {
            popup.ShowPopup(index);
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (backgroundImage != null)
        {
            backgroundImage.sprite = selected ? selectedSprite : normalSprite;
        }
    }

    private void OnDestroy()
    {
        if (selectButton != null)
        {
            selectButton.onClick.RemoveListener(OnClicked);
        }
    }
}