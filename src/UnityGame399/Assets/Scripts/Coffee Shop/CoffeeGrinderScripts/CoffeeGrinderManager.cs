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
        
        currentClicks++;
        Logger.Instance.Info($"Grinding beans: {currentClicks}/{clicksRequired}");
        
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
            
            CoffeeShopManager.Instance.CompleteGrinding(beanType);
        }

        SpawnGrindPopup();
        
        currentClicks = 0;
        lastSelectedIndex = -1;
    }

    private void SpawnGrindPopup()
    {
        if (grindPopupPrefab == null || popupParent == null)
            return;

        string beanType = lastSelectedIndex == 0 ? "Decaf" : "Caffeinated";

        GameObject popup = Instantiate(grindPopupPrefab, popupParent);
        popup.transform.localPosition = Vector3.zero;

        popup.GetComponent<TextFloat>().Show($"+1 {beanType} Grounds");
    }
    
    public int GetCurrentClicks() => currentClicks;
    public float GetProgress() => (float)currentClicks / clicksRequired;
}