using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class Trash : Interactable
{
    public TrashCan trashCan;
    public AudioClip littering;
    private void Start()
    {
        SoundManager.Instance.playOtherSFX(littering);
    }
    public override void Interact()
    {
        Logger.Instance.Info("Interacted with Trash");
        trashCan.AddTrashToCan();
        Destroy(this.gameObject);
    }
}