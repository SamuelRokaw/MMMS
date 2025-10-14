using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private int newLayer = 2;
    [SerializeField] private int oldLayer = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.sortingOrder = newLayer;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sortingOrder = oldLayer;
    }
}
