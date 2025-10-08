using System;
using UnityEngine;

public class OverworldInteraction : MonoBehaviour
{
    public GameObject interactIcon;
    private GameObject interactTarget;

    
    private void Awake()
    {
        interactIcon.SetActive(false);
    }

    public void Interact()
    {
        if (interactTarget!= null)
        {
            if (interactTarget.tag == "Aquarium")
            {
                interactTarget.GetComponent<Aquarium>().Interact();
            }
            else if (interactTarget.tag == "Upgrade")
            {
                interactTarget.GetComponent<UpgradeItem>().Interact();
            }
            else if (interactTarget.tag == "LG")
            {
                interactTarget.GetComponent<LevelGate>().Interact();
            }
            CloseInteractableIcon();
        }
    }
    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }
    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    public void setinteractTarget(GameObject target)
    {
        interactTarget = target;
    }

    public void removeInteractTarget()
    {
        interactTarget = null;
    }
}
