using UnityEngine;
using UnityEngine.UI;

public class InventorySlidePanel : MonoBehaviour
{
    public RectTransform panelToSlide;
    public Button slideToggleButton; 
    
    public float slideSpeed = 5f;
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public bool startHidden = true;

    private bool isShown = false;

    private void Start()
    {
        if (slideToggleButton != null)
        {
            slideToggleButton.onClick.AddListener(TogglePanel);
        }
        
        if (panelToSlide != null)
        {
            isShown = !startHidden;
            panelToSlide.anchoredPosition = isShown ? shownPosition : hiddenPosition;
        }
    }

    private void Update()
    {
        if (panelToSlide != null)
        {
            Vector2 targetPos = isShown ? shownPosition : hiddenPosition;
            panelToSlide.anchoredPosition = Vector2.Lerp(
                panelToSlide.anchoredPosition,
                targetPos,
                Time.deltaTime * slideSpeed
            );
        }
    }

    public void TogglePanel()
    {
        if (isShown)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    public void ShowPanel()
    {
        isShown = true;
    }

    public void HidePanel()
    {
        isShown = false;
        StateManager.Instance.SwitchToCoffeeShop();
    }

    public bool IsShown() => isShown;

    private void OnDestroy()
    {
        if (slideToggleButton != null)
        {
            slideToggleButton.onClick.RemoveListener(TogglePanel);
        }
    }
}