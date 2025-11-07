using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Manages the coffee topping minigame where players pour creamers.
/// Attach this to the CoffeeToppingGame object.
/// </summary>
public class CoffeeTopper : MonoBehaviour
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
        if (CheckSelections() == false)
        {
            return;
        }
        
        if (!isLocked)
        {
            isLocked = true;
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
        if (currentPercentage <= 0)
        {
            return;
        }

        CollectCoffee();
    }

    private void CollectCoffee()
    {
        BeanType brewType = brewSelector.GetSelectedBeanType();
        CreamerType creamerType = creamerSelector.GetSelectedCreamerType();
        double creamPercent = currentPercentage / 100.0;

        Coffee coffee = new Coffee(brewType, creamPercent, creamerType);
        
        if (CoffeeShopManager.Instance != null)
        {
            CoffeeShopManager.Instance.CompleteTopping(coffee, brewType);
        }

        Logger.Instance.Info("Collected coffee");

        ResetMinigame();
    }

    private void ResetMinigame()
    {
        currentPercentage = 0f;
        isLocked = false;
        isPouring = false;
        hasCreamAdded = false;
        
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

        // NEED TO DO COFFEE FILLING VISUAL HERE
    }

    public float GetCurrentPercentage() => currentPercentage;
}