using UnityEngine;

[System.Serializable]
public class Dumpster : Interactable
{
    [SerializeField] public GameObject trashBag;

    public override void Interact()
    {
        Logger.Instance.Info("Interacted with Dumpster");
        trashBag.SetActive(false);
        StateManager.Instance.SwitchToCoffeeShop();
    }
}
