using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupToggler : MonoBehaviour
{
    public CanvasGroup currentCanvasGroup;
    public void TurnOnCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public void TurnOffCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    
    public void SwitchCanvasGroup(CanvasGroup canvasGroup)
    {
        if (currentCanvasGroup != canvasGroup)
        {
            TurnOffCanvasGroup(currentCanvasGroup);
            TurnOnCanvasGroup(canvasGroup);
            currentCanvasGroup = canvasGroup;
        }
    }
}
