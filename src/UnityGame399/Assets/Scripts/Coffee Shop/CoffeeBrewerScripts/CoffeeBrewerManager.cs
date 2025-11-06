using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoffeeBrewerManager : MonoBehaviour
{
    public Image brewerImage;
    public Sprite idleSprite;
    public Sprite brewingSprite;
    public Sprite completeSprite;
    
    public CoffeeBrewerListManager groundsDisplay;
    public Button brewButton;
    public Button collectButton;
    public TextMeshProUGUI timerText; 
    
    public int brewerID = 0; 

    private void Start()
    {
        if (brewButton != null)
        {
            brewButton.onClick.AddListener(OnBrewClicked);
        }
        
        if (collectButton != null)
        {
            collectButton.onClick.AddListener(OnCollectClicked);
            collectButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (CoffeeBrewerTimeTracker.Instance == null) return;

        bool isBrewing = CoffeeBrewerTimeTracker.Instance.IsBrewerActive(brewerID);
        bool isComplete = CoffeeBrewerTimeTracker.Instance.IsBrewerComplete(brewerID);
        float timeRemaining = CoffeeBrewerTimeTracker.Instance.GetBrewerTimeRemaining(brewerID);
        
        if (brewerImage != null)
        {
            if (isComplete)
            {
                brewerImage.sprite = completeSprite;
            }
            else if (isBrewing)
            {
                brewerImage.sprite = brewingSprite;
            }
            else
            {
                brewerImage.sprite = idleSprite;
            }
        }
        
        if (brewButton != null)
        {
            brewButton.interactable = !isBrewing && !isComplete;
        }
        
        
        if (timerText != null)
        {
            if (isBrewing)
            {
                timerText.text = $"Brewing: {Mathf.CeilToInt(timeRemaining)}s";
            }
            else if (isComplete)
            {
                timerText.text = "Ready!";
            }
            else
            {
                timerText.text = "";
            }
        }
        
        if (collectButton != null)
        {
            collectButton.gameObject.SetActive(isComplete);
        }
    }

    private void OnBrewClicked()
    {
        if (!groundsDisplay.HasSelection())
        {
            Logger.Instance.Info("Please select coffee grounds first!");
            return;
        }

        if (CoffeeBrewerTimeTracker.Instance.IsBrewerActive(brewerID))
        {
            Logger.Instance.Info("This brewer is already brewing!");
            return;
        }

        BeanType selectedType = groundsDisplay.GetSelectedBeanType();
        
        bool success = CoffeeBrewerTimeTracker.Instance.StartBrewing(brewerID, selectedType);
        
        if (success)
        {
            Logger.Instance.Info($"Started brewing {selectedType} coffee!");
            groundsDisplay.DeselectAll();
            groundsDisplay.UpdateGroundsDisplay();
        }
        else
        {
            Logger.Instance.Info("Failed to start brewing - not enough grounds!");
        }
    }

    private void OnCollectClicked()
    {
        if (!CoffeeBrewerTimeTracker.Instance.IsBrewerComplete(brewerID))
        {
            return;
        }

        BeanType brewedType = CoffeeBrewerTimeTracker.Instance.CollectBrew(brewerID);
        Logger.Instance.Info($"Collected {brewedType} coffee!");
        
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (brewButton != null)
        {
            brewButton.onClick.RemoveListener(OnBrewClicked);
        }
        
        if (collectButton != null)
        {
            collectButton.onClick.RemoveListener(OnCollectClicked);
        }
    }
}