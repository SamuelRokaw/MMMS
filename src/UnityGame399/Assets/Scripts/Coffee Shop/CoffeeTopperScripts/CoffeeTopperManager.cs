using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CoffeeTopperManager : MonoBehaviour
{
    public CoffeeToppingBrewListManager brewSelector;
    public CoffeeCreamerListManager creamerSelector;
    public Image coffeeCup;
    public TextMeshProUGUI percentageText;
    public Button coffeeCupButton;
    public Button collectButton; 
    
    public float fillRate = 5f;
    public float maxPercentage = 100f;

    private float currentPercentage = 0f;
    private bool isPouring = false;
    private bool isLocked = false;
    private bool hasCreamAdded = false;
    private int selectedCoffeeIndex = -1;
    
    private void Start()
    {
        if (coffeeCupButton != null)
        {
            EventTrigger trigger = coffeeCupButton.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = coffeeCupButton.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;
            entryDown.callback.AddListener((data) => { OnCupPressed(); });
            trigger.triggers.Add(entryDown);

            EventTrigger.Entry entryUp = new EventTrigger.Entry();
            entryUp.eventID = EventTriggerType.PointerUp;
            entryUp.callback.AddListener((data) => { OnCupReleased(); });
            trigger.triggers.Add(entryUp);
        }

        if (collectButton != null)
        {
            collectButton.onClick.AddListener(OnCollectClicked);
            collectButton.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    private void Update()
    {
        if (isPouring)
        {
            currentPercentage += fillRate * Time.deltaTime;
            currentPercentage = Mathf.Min(currentPercentage, maxPercentage);
            
            if (currentPercentage > 0 && !hasCreamAdded)
            {
                hasCreamAdded = true;
                if (collectButton != null)
                {
                    collectButton.gameObject.SetActive(true);
                }
            }

            UpdateUI();
            
            if (currentPercentage >= maxPercentage)
            {
                isPouring = false;
            }
        }
    }

    private void OnCupPressed()
    {
        if (!CheckSelections())
        {
            return;
        }
        
        if (!isLocked)
        {
            isLocked = true;
            
            selectedCoffeeIndex = brewSelector.GetFirstPlainCoffeeIndex();
            
            if (selectedCoffeeIndex < 0)
            {
                Logger.Instance.Info("No plain coffee of this type available!");
                isLocked = false;
                return;
            }
            
            creamerSelector.LockSelection();
            brewSelector.LockSelection();
            Logger.Instance.Info("Pour has been started.");
        }

        isPouring = true;
    }
    
    private bool CheckSelections() 
    {
        if (!brewSelector.HasSelection())
        {
            Logger.Instance.Info("Please select a brew first!");
            return false;
        }
    
        if (!creamerSelector.HasSelection())
        {
            Logger.Instance.Info("Please select a creamer first!");
            return false;
        }
        
        return true;
    }
    
    private void OnCupReleased()
    {
        isPouring = false;
    }

    private void OnCollectClicked()
    {
        if (currentPercentage <= 0 || selectedCoffeeIndex < 0)
        {
            return;
        }

        CollectCoffee();
    }

    private void CollectCoffee()
    {
        if (CoffeeShopManager.Instance == null || selectedCoffeeIndex < 0 || 
            selectedCoffeeIndex >= CoffeeShopManager.Instance.coffees.Count)
        {
            Logger.Instance.Info("Invalid coffee selection");
            return;
        }

        CreamerType creamerType = creamerSelector.GetSelectedCreamerType();
        double creamPercent = currentPercentage / 100.0;
        
        Coffee coffee = CoffeeShopManager.Instance.coffees[selectedCoffeeIndex];
        coffee.CreamPercent = creamPercent;
        coffee.CreamerType = creamerType;
        
        CoffeeShopManager.Instance.OnCoffeeAdded?.Invoke();

        ResetMinigame();
    }

    private void ResetMinigame()
    {
        currentPercentage = 0f;
        isLocked = false;
        isPouring = false;
        hasCreamAdded = false;
        selectedCoffeeIndex = -1;
        
        brewSelector.UnlockSelection();
        brewSelector.DeselectAll();
        
        creamerSelector.UnlockSelection();
        creamerSelector.DeselectAll();

        if (collectButton != null)
        {
            collectButton.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (percentageText != null)
        {
            percentageText.text = $"{Mathf.RoundToInt(currentPercentage)}%";
        }
    }

    public float GetCurrentPercentage() => currentPercentage;
}