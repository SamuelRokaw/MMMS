using UnityEngine;

public class InfoSign : Interactable
{
    public GameObject infoDialog;

    public override void Interact()
    {
            Debug.Log("Info Sign Interacted");
            infoDialog.SetActive(true);
    }
}
