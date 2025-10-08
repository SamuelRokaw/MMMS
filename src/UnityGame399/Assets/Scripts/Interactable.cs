using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool used = false;
    public abstract void Interact();

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OverWorldPlayer") && !used)
        {
            collision.gameObject.GetComponent<OverworldInteraction>().OpenInteractableIcon();
            collision.gameObject.GetComponent<OverworldInteraction>().setinteractTarget(this.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OverWorldPlayer"))
        {
            collision.gameObject.GetComponent<OverworldInteraction>().CloseInteractableIcon();
            collision.gameObject.GetComponent<OverworldInteraction>().removeInteractTarget();
        }
    }
}
