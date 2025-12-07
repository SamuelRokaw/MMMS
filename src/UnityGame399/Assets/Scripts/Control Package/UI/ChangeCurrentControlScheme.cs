using UnityEngine;
using System;
public class ChangeCurrentControlScheme : MonoBehaviour
{
    public static Action updateControlUI;
    
    public void ChangeToKeyboard()
    {
        PlayerControlsManager.Instance.currentControlScheme = "Keyboard";
        updateControlUI.Invoke();
    }

    public void ChangeToGamePad()
    {
        PlayerControlsManager.Instance.currentControlScheme = "GamePad";
        updateControlUI.Invoke();
    }

    public void ResetControls()
    {
        PlayerControlsManager.Instance.ResetRebinds();
        updateControlUI.Invoke();
    }
}
