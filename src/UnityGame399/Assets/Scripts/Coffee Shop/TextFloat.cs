using UnityEngine;
using TMPro;
using System.Collections;

public class TextFloat : MonoBehaviour
{
    public float lifetime = 1f;
    public float floatDistance = 50f;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(string message)
    {
        text.text = message;
        startPos = transform.localPosition;

        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        float timer = 0f;
        Vector3 endPos = startPos + new Vector3(0, floatDistance, 0);

        while (timer < lifetime)
        {
            timer += Time.deltaTime;
            float t = timer / lifetime;

            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            canvasGroup.alpha = 1f - t;

            yield return null;
        }

        Destroy(gameObject);
    }
}