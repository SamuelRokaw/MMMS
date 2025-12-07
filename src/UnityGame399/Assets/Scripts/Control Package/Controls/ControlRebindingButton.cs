using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class ControlRebindingButton : MonoBehaviour
{
    public string actionName;
    public Button rebindButton;
    public TextMeshProUGUI buttonLabel;
    public string compositePart; // e.g. "Up", "Down", "Left", "Right"


    private GameObject lastSelected;
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        ChangeCurrentControlScheme.updateControlUI += UpdateLabel;
    }

    private void Unsubscribe()
    {
        ChangeCurrentControlScheme.updateControlUI -= UpdateLabel;
    }
    private void Start()
    {
        rebindButton.onClick.AddListener(StartRebind);
        UpdateLabel();
    }

    private void StartRebind()
    {
        // Cache the currently selected UI element
        lastSelected = EventSystem.current.currentSelectedGameObject;

        rebindButton.interactable = false;
        buttonLabel.text = "Press a key...";

        if (string.IsNullOrEmpty(compositePart))
        {
            PlayerControlsManager.Instance.RebindAction(actionName, () =>
            {
                FinishRebind();
            });
        }
        else
        {
            PlayerControlsManager.Instance.RebindComposite(actionName, compositePart, () =>
            {
                FinishRebind();
            });
        }
    }

    private void FinishRebind()
    {
        UpdateLabel();
        rebindButton.interactable = true;

        // Restore selection so UI navigation works again
        if (lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }

    private void UpdateLabel()
    {
        var action = PlayerControlsManager.Instance.GetAction(actionName);
        if (action == null) return;

        int bindingIndex;

        if (string.IsNullOrEmpty(compositePart))
        {
            bindingIndex = PlayerControlsManager.FindBindingIndexForScheme(action, 
                PlayerControlsManager.Instance.currentControlScheme);
        }
        else
        {
            bindingIndex = PlayerControlsManager.FindCompositePartIndexForScheme(action, compositePart, 
                PlayerControlsManager.Instance.currentControlScheme);
        }

        if (bindingIndex >= 0)
        {
            buttonLabel.text = action.GetBindingDisplayString(bindingIndex);
        }
        else
        {
            buttonLabel.text = "Unbound";
        }
    }

}


