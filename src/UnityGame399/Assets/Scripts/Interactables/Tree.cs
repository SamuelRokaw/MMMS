using UnityEngine;

public class Tree : Interactable
{
    public GameObject Dialog;
    public string treename;
    

    public override void Interact()
    {
        if (!used)
        {
            used = true;
            Logger.Instance.Info($"{treename} interacted");
            Dialog.SetActive(true);
        }
    }
}
