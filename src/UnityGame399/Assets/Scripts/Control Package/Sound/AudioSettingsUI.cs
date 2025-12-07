using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider masterSlider;
    public TMP_InputField masterInput;

    public Slider backgroundSlider;
    public TMP_InputField backgroundInput;

    public Slider playerSlider;
    public TMP_InputField playerInput;

    public Slider enemySlider;
    public TMP_InputField enemyInput;

    public Slider otherSlider;
    public TMP_InputField otherInput;

    void Start()
    {
        // Initialize from PlayerPrefs
        InitSlider(masterSlider, masterInput, PlayerPrefs.GetFloat("MasterVolume", 1f) * 100f);
        InitSlider(backgroundSlider, backgroundInput, PlayerPrefs.GetFloat("BackgroundVolume", 1f) * 100f);
        InitSlider(playerSlider, playerInput, PlayerPrefs.GetFloat("PlayerVolume", 1f) * 100f);
        InitSlider(enemySlider, enemyInput, PlayerPrefs.GetFloat("EnemyVolume", 1f) *100f);
        InitSlider(otherSlider, otherInput, PlayerPrefs.GetFloat("OtherVolume", 1f) * 100f);

        // Hook events
        masterSlider.onValueChanged.AddListener(v => OnSliderChanged(v, masterInput, "MasterVolume", SoundManager.Instance.SetMasterVolume));
        backgroundSlider.onValueChanged.AddListener(v => OnSliderChanged(v, backgroundInput, "BackgroundVolume", SoundManager.Instance.SetBackgroundVolume));
        playerSlider.onValueChanged.AddListener(v => OnSliderChanged(v, playerInput, "PlayerVolume", SoundManager.Instance.SetPlayerVolume));
        enemySlider.onValueChanged.AddListener(v => OnSliderChanged(v, enemyInput, "EnemyVolume", SoundManager.Instance.SetEnemyVolume));
        otherSlider.onValueChanged.AddListener(v => OnSliderChanged(v, otherInput, "OtherVolume", SoundManager.Instance.SetOtherVolume));

        masterInput.onEndEdit.AddListener(t => OnInputChanged(t, masterSlider, "MasterVolume", SoundManager.Instance.SetMasterVolume));
        backgroundInput.onEndEdit.AddListener(t => OnInputChanged(t, backgroundSlider, "BackgroundVolume", SoundManager.Instance.SetBackgroundVolume));
        playerInput.onEndEdit.AddListener(t => OnInputChanged(t, playerSlider, "PlayerVolume", SoundManager.Instance.SetPlayerVolume));
        enemyInput.onEndEdit.AddListener(v => OnInputChanged(v, enemySlider, "EnemyVolume", SoundManager.Instance.SetEnemyVolume));
        otherInput.onEndEdit.AddListener(t => OnInputChanged(t, otherSlider, "OtherVolume", SoundManager.Instance.SetOtherVolume));
    }

    void InitSlider(Slider slider, TMP_InputField input, float value)
    {
        slider.value = value;
        input.text = Mathf.RoundToInt(value).ToString();
    }

    void OnSliderChanged(float value, TMP_InputField input, string key, System.Action<float> applyVolume)
    {
        input.text = Mathf.RoundToInt(value).ToString();
        float normalized = value / 100f;
        applyVolume(normalized);
        PlayerPrefs.SetFloat(key, normalized);
        PlayerPrefs.Save();
    }

    void OnInputChanged(string text, Slider slider, string key, System.Action<float> applyVolume)
    {
        if (int.TryParse(text, out int val))
        {
            val = Mathf.Clamp(val, 0, 100);
            slider.value = val;
            float normalized = val / 100f;
            applyVolume(normalized);
            PlayerPrefs.SetFloat(key, normalized);
            PlayerPrefs.Save();
        }
    }
}

