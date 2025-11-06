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
            else if (interactTarget.tag == "infostand")
            {
                interactTarget.GetComponent<InfoSign>().Interact();
            }
            else if (interactTarget.tag == "Tree")
            {
                interactTarget.GetComponent<Tree>().Interact();
            }
            else if (interactTarget.tag == "ControlTree")
            {
                interactTarget.GetComponent<ControlTree>().Interact();
            }
            else if (interactTarget.tag == "NPC")
            {
                interactTarget.GetComponent<NPC>().Interact();
            }
            else if (interactTarget.tag == "BA")
            {
                interactTarget.GetComponent<BossAquarium>().Interact();
            }
            else if (interactTarget.tag == "CoffeeGrinder")
            {
                interactTarget.GetComponent<CoffeeGame>().Interact();
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
