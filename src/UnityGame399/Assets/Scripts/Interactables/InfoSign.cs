using UnityEngine;

public class InfoSign : Interactable
{
    public GameObject infoDialog;

    public override void Interact()
    {
            Logger.Instance.Info("Info Sign Interacted");
            infoDialog.SetActive(true);
    }
}
