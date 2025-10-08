using UnityEngine;

public class Aquarium : Interactable
{
    public GameObject combatEncounterPrefab;
    public Sprite filledSprite;
    public Sprite emptySprite;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        setSprite();
    }

    public override void Interact()
    {
        if (!used)
        {
            used = true;
            Debug.Log("Aquarium interact");
            //Instantiate(combatEncounterPrefab);
            setSprite();
        }
    }



    private void setSprite()
    {
        if (used)
        {
            spriteRenderer.sprite = emptySprite;
        }
        else
        {
            spriteRenderer.sprite = filledSprite;
        }
    }
}
