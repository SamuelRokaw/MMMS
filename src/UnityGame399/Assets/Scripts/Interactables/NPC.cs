using UnityEngine;

public class NPC :Interactable
{
    public GameObject Dialog;
    public string npcName;

    public override void Interact()
    {
        Logger.Instance.Info($"{npcName} interacted");
        Dialog.SetActive(true);
    }
}
