using UnityEngine;

public class InfoSign : Interactable
{
    public GameObject infoPrefab;

    public override void Interact()
    {
            Debug.Log("Info Sign Interacted");
            Instantiate(infoPrefab);
    }
}
