using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool used = false;
    public abstract void Interact();

    
    private void OnCollisionStay2D(Collision2D collision)//changed from enter to stay to fix interactable issues
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
    private void OnTriggerStay2D(Collider2D collision)//changed from enter to stay to fix interactable issues
    {
        if (collision.gameObject.CompareTag("OverWorldPlayer") && !used)
        {
            collision.gameObject.GetComponent<OverworldInteraction>().OpenInteractableIcon();
            collision.gameObject.GetComponent<OverworldInteraction>().setinteractTarget(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OverWorldPlayer"))
        {
            collision.gameObject.GetComponent<OverworldInteraction>().CloseInteractableIcon();
            collision.gameObject.GetComponent<OverworldInteraction>().removeInteractTarget();
        }
    }
}
