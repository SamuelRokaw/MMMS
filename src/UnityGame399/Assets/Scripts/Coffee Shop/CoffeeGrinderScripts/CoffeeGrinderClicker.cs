using System.Collections;
using UnityEngine;

public class CoffeeGrinderClicker : MonoBehaviour // stole all of this shit from a tutorial
{
    public float pulseScaleMultiplier = 1.2f; 
    public float pulseDuration = 0.2f;

    private Vector3 originalScale;
    private bool isPulsing = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnMouseDown()
    {
        if (!isPulsing)
        {
            StartCoroutine(PulseEffect());
        }
    }

    IEnumerator PulseEffect()
    {
        isPulsing = true;
        
        Vector3 targetScale = originalScale * pulseScaleMultiplier;
        float timer = 0f;
        while (timer < pulseDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (pulseDuration / 2f));
            timer += Time.deltaTime;
            yield return null;
        }
        
        timer = 0f;
        while (timer < pulseDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (pulseDuration / 2f));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        isPulsing = false;
    }
}
