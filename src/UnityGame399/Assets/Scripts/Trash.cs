using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class Trash : Interactable
{
    public TrashCan trashCan;
    public override void Interact()
    {
        Logger.Instance.Info("Interacted with Trash");
        trashCan.AddTrashToCan();
        Destroy(this.gameObject);
    }
}