using System;
using PlayerStuff;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransitionStateUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public CanvasGroup canvasGroup;

    public void Start()
    {
        if (StateManager.Instance.currentShopState == ShopStates.Transition)
        {
            ShopStateEvent.TransitionState.Invoke();
        }
    }

    public void OnEnable()
    {
        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        ShopStateEvent.TransitionState += ActivateCanvasGroup;
    }

    private void Unsubscribe()
    {
        ShopStateEvent.TransitionState -= ActivateCanvasGroup;
    }

    public void ActivateCanvasGroup()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        SetResultText();
    }

    public void DeactivateCanvasGroup()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    
    public void SetResultText()
    {
        if (StateManager.Instance.nextShopState == ShopStates.DayTime)
        {
            text.text = "Night Completed";
        }
        else
        {
            text.text = "Day Completed";
        }
    }
    
    
    public void Continue()
    {
        if (StateManager.Instance.nextShopState == ShopStates.DayTime)
        {
            StateManager.Instance.SwitchShopToDay();
        }
        else
        {
            StateManager.Instance.SwitchShopToNight();
        }
        DeactivateCanvasGroup();
    }
}
