using UnityEngine;
using TMPro;

public class TextFloat : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float floatSpeed = 50f; 
    public float fadeSpeed = 1f; 
    public float lifetime = 2f;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private float timer = 0f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        rectTransform.anchoredPosition += Vector2.up * floatSpeed * Time.deltaTime;
        
        canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
        
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetText(string text)
    {
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
    
    public void SetColor(Color color)
    {
        if (textComponent != null)
        {
            textComponent.color = color;
        }
    }
}