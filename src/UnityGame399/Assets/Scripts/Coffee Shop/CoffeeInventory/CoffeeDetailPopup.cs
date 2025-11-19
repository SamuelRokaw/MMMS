using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CoffeeDetailPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI beanTypeText;
    public TextMeshProUGUI creamerTypeText;
    public TextMeshProUGUI percentageText;
    public TextMeshProUGUI coffeeNumberText;
    public Button submitButton;
    public Button nextButton;
    public Button previousButton;
    public Button closeButton;

    private int currentIndex = 0;

    private void Start()
    {
        HidePopup();
        
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitClicked);
        }
        
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextCoffee);
        }
        
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(ShowPreviousCoffee);
        }
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HidePopup);
        }
    }

    public void ShowPopup(int startIndex = 0)
    {
        if (CoffeeShopManager.Instance == null || CoffeeShopManager.Instance.coffees.Count == 0)
        {
            Logger.Instance.Info("No coffees in inventory!");
            return;
        }

        currentIndex = Mathf.Clamp(startIndex, 0, CoffeeShopManager.Instance.coffees.Count - 1);
        
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
        
        UpdateDisplay();
    }

    public void HidePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    private void UpdateDisplay()
    {
        if (CoffeeShopManager.Instance == null || CoffeeShopManager.Instance.coffees.Count == 0)
        {
            HidePopup();
            return;
        }

        List<Coffee> coffees = CoffeeShopManager.Instance.coffees;
        
        currentIndex = Mathf.Clamp(currentIndex, 0, coffees.Count - 1);
        
        Coffee currentCoffee = coffees[currentIndex];
        
        if (beanTypeText != null)
        {
            beanTypeText.text = $"Bean: {currentCoffee.BeanType}";
        }

        if (creamerTypeText != null)
        {
            creamerTypeText.text = $"Creamer: {currentCoffee.CreamerType}";
        }

        if (percentageText != null)
        {
            percentageText.text = $"Amount: {(currentCoffee.CreamPercent * 100):F0}%";
        }

        if (coffeeNumberText != null)
        {
            coffeeNumberText.text = $"Coffee {currentIndex + 1} of {coffees.Count}";
        }
        
        if (previousButton != null)
        {
            previousButton.interactable = (currentIndex > 0);
        }

        if (nextButton != null)
        {
            nextButton.interactable = (currentIndex < coffees.Count - 1);
        }
    }

    public void ShowNextCoffee()
    {
        if (CoffeeShopManager.Instance == null) return;
        
        if (currentIndex < CoffeeShopManager.Instance.coffees.Count - 1)
        {
            currentIndex++;
            UpdateDisplay();
        }
    }

    public void ShowPreviousCoffee()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateDisplay();
        }
    }

    private void OnSubmitClicked()
    {
        if (CoffeeShopManager.Instance == null || CoffeeShopManager.Instance.coffees.Count == 0)
        {
            Logger.Instance.Info("No coffee to submit!");
            return;
        }

        Coffee coffeeToSubmit = CoffeeShopManager.Instance.coffees[currentIndex];
        
        CustomerManager customerManager = FindFirstObjectByType<CustomerManager>();
        if (customerManager != null)
        {
            customerManager.SubmitCoffee(coffeeToSubmit);
            
            CoffeeShopManager.Instance.coffees.RemoveAt(currentIndex);
            CoffeeShopManager.Instance.OnCoffeeAdded?.Invoke(); 
            

            if (CoffeeShopManager.Instance.coffees.Count == 0)
            {
                HidePopup();
            }
            else
            {
                if (currentIndex >= CoffeeShopManager.Instance.coffees.Count)
                {
                    currentIndex = CoffeeShopManager.Instance.coffees.Count - 1;
                }
                UpdateDisplay();
            }
        }
    }

    private void OnDestroy()
    {
        if (submitButton != null)
        {
            submitButton.onClick.RemoveListener(OnSubmitClicked);
        }
        
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(ShowNextCoffee);
        }
        
        if (previousButton != null)
        {
            previousButton.onClick.RemoveListener(ShowPreviousCoffee);
        }
        
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(HidePopup);
        }
    }
}