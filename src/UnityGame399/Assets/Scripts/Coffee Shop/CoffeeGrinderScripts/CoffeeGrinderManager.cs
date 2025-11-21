using UnityEngine;
using UnityEngine.UI;
public class CoffeeGrinderManager : MonoBehaviour
{
    public CoffeeGrinderTypeListManager coffeeTypeSelector;
    public Button grindButton;

    public int clicksRequired = 15;

    private int currentClicks = 0;
    private int lastSelectedIndex = -1;
    

    public GameObject grindPopupPrefab;
    public RectTransform popupParent;

    public AudioClip[] grindingSounds;
    public AudioClip completionSound;

    public Stats stats;

    private void Start()
    {
        if (grindButton != null)
        {
            grindButton.onClick.AddListener(OnGrindClicked);
        }
    }

    private void OnGrindClicked()
    {
        if (!coffeeTypeSelector.HasSelection())
        {
            Logger.Instance.Info("No coffee type selected. Can not grind beans.");
            return;
        }

        int currentSelectedIndex = coffeeTypeSelector.GetSelectedIndex();
        
        if (lastSelectedIndex != -1 && lastSelectedIndex != currentSelectedIndex)
        {
            Logger.Instance.Info("Coffee type changed. Resetting clicks.");
            currentClicks = 0;
        }
        
        lastSelectedIndex = currentSelectedIndex;
        BeanType beanType = lastSelectedIndex == 0 ? BeanType.Decaf : BeanType.Caffeinated;
        switch (beanType)
        {
            case BeanType.Caffeinated:
                if (stats.currentCafBean < 5)
                {
                    Logger.Instance.Info("Not enough beans of selected coffee type. Can not grind beans.");
                    return;
                }
                break;
            case BeanType.Decaf:
                if (stats.currentDecafBean < 5)
                {
                    Logger.Instance.Info("Not enough beans of selected coffee type. Can not grind beans.");
                    return;
                }

                break;
        }
        
        currentClicks++;
        Logger.Instance.Info($"Grinding beans: {currentClicks}/{clicksRequired}");
        
        if (grindingSounds != null && grindingSounds.Length > 0 && SoundManager.Instance != null)
        {
            AudioClip randomGrind = grindingSounds[UnityEngine.Random.Range(0, grindingSounds.Length)];
            SoundManager.Instance.playOtherSFX(randomGrind);
        }
        
        if (currentClicks >= clicksRequired)
        {
            CompleteGrinding();
        }
    }

    private void CompleteGrinding()
    {
        string coffeeName = coffeeTypeSelector.GetSelectedName();
        Logger.Instance.Info($"Grinding complete! {coffeeName} grounds are ready.");
        
        if (CoffeeShopManager.Instance != null)
        {
            BeanType beanType = lastSelectedIndex == 0 ? BeanType.Decaf : BeanType.Caffeinated;
            PlayerStatEvents.PlayerBeans(5, beanType);
            CoffeeShopManager.Instance.CompleteGrinding(beanType);
            
            if (completionSound != null && SoundManager.Instance != null)
            {
                SoundManager.Instance.playOtherSFX(completionSound);
            }
            
            string displayText = $"+1 {beanType} Grounds";
            Color textColor = beanType == BeanType.Decaf ? Color.cyan : new Color(1f, 0.5f, 0f);
        
            FloatingTextSpawner.Instance?.SpawnTextAtUI(displayText, transform, textColor);
        }
        
        
        currentClicks = 0;
        lastSelectedIndex = -1;
    }
    
    public int GetCurrentClicks() => currentClicks;
    public float GetProgress() => (float)currentClicks / clicksRequired;
}