using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;

public class PlayerControlsManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap actionMap;
    private InputDevice currentDevice;

    private const string RebindsKey = "InputRebinds";
    public string currentControlScheme = "Keyboard";
    public static PlayerControlsManager Instance {get; private set;}
    
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        actionMap = inputActions.FindActionMap("Player");
        actionMap.Enable();

        LoadRebinds();
    }
    
    void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (device == null) return;

        // Detect if device changed
        if (currentDevice != device)
        {
            currentDevice = device;
            Debug.Log("Switched to: " + device.displayName);
        }
    }

    public InputAction GetAction(string actionName)
    {
        return actionMap.FindAction(actionName);
    }

    public void RebindAction(string actionName, System.Action onComplete)
    {
        var action = GetAction(actionName);
        if (action == null) return;

        // Find binding index for current scheme
        int bindingIndex = FindBindingIndexForScheme(action, currentControlScheme);
        if (bindingIndex == -1)
        {
            Debug.LogWarning($"No binding for scheme '{currentControlScheme}' in action '{actionName}'");
            return;
        }

        action.Disable();

        action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();

                SaveRebinds();
                onComplete?.Invoke();
            })
            .Start();
    }

    public void RebindComposite(string actionName, string compositePart, System.Action onComplete)
    {
        var action = GetAction(actionName);
        if (action == null) return;

        int bindingIndex = FindCompositePartIndexForScheme(action, compositePart, currentControlScheme);
        if (bindingIndex == -1)
        {
            Debug.LogWarning($"Composite part '{compositePart}' not found for scheme '{currentControlScheme}' in action '{actionName}'");
            return;
        }

        action.Disable();

        action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                SaveRebinds();
                onComplete?.Invoke();
            })
            .Start();
    }

    // Helpers
    public static int FindBindingIndexForScheme(InputAction action, string scheme)
    {
        var bindings = action.bindings;
        for (int i = 0; i < bindings.Count; i++)
        {
            var b = bindings[i];
            if (!b.isComposite && !b.isPartOfComposite && b.groups.Contains(scheme))
                return i;
        }
        return -1;
    }

    public static int FindCompositePartIndexForScheme(InputAction action, string partName, string scheme)
    {
        var bindings = action.bindings;
        for (int i = 0; i < bindings.Count; i++)
        {
            var b = bindings[i];
            if (b.isPartOfComposite && b.name == partName && b.groups.Contains(scheme))
                return i;
        }
        return -1;
    }



    private void SaveRebinds()
    {
        string rebinds = inputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(RebindsKey, rebinds);
        PlayerPrefs.Save();
        Debug.Log("Controls saved: " + rebinds);
    }

    private void LoadRebinds()
    {
        if (PlayerPrefs.HasKey(RebindsKey))
        {
            string rebinds = PlayerPrefs.GetString(RebindsKey);
            inputActions.LoadBindingOverridesFromJson(rebinds);
            Debug.Log("Controls loaded: " + rebinds);
        }
    }

    public void ResetRebinds()
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(RebindsKey);
        Debug.Log("Controls reset to defaults.");
    }

    public void vibrateController(float low, float high)
    {
        if(isCurrentDeviceGamePad())
        {
            Gamepad.current.SetMotorSpeeds(low, high);
        }
    }

    public void stopVibrateController()
    {
        if (isCurrentDeviceGamePad())
        {
            InputSystem.ResetHaptics();
        }
    }

    public bool isCurrentDeviceGamePad()
    {
        return currentDevice is Gamepad;
    }
    
}

