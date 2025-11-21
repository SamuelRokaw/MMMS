using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;
    
    public GameObject floatingTextPrefab;
    
    public Transform spawnParent;
    public Vector3 spawnOffset = Vector3.zero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SpawnText(string text, Vector3 worldPosition, Color? color = null)
    {
        if (floatingTextPrefab == null)
        {
            return;
        }
        
        if (spawnParent == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                spawnParent = canvas.transform;
            }
        }
        
        GameObject popup = Instantiate(floatingTextPrefab, spawnParent);
        
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition + spawnOffset);
            rectTransform.position = screenPoint;
        }
        
        TextFloat floatingText = popup.GetComponent<TextFloat>();
        if (floatingText != null)
        {
            floatingText.SetText(text);
            if (color.HasValue)
            {
                floatingText.SetColor(color.Value);
            }
        }
    }

    public void SpawnTextAtUI(string text, Transform uiElement, Color? color = null)
    {
        if (floatingTextPrefab == null || uiElement == null)
        {
            return;
        }

        if (spawnParent == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas != null)
            {
                spawnParent = canvas.transform;
            }
        }

        GameObject popup = Instantiate(floatingTextPrefab, spawnParent);
        
        TextFloat floatingText = popup.GetComponent<TextFloat>();
        if (floatingText != null)
        {
            floatingText.SetText(text);
            if (color.HasValue)
            {
                floatingText.SetColor(color.Value);
            }
        }
        
        RectTransform rectTransform = popup.GetComponent<RectTransform>();
        RectTransform uiRect = uiElement.GetComponent<RectTransform>();

        if (rectTransform != null && uiRect != null)
        {
            rectTransform.position = uiRect.position + spawnOffset;
        }
    }
}