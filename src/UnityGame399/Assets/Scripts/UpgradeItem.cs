using Unity.VisualScripting;
using UnityEngine;
using System;

public class UpgradeItem : Interactable
{
    public string upgradeName;
    public static Action<string> upgradeStat;

    private void Awake()
    {
        used = false;
    }
    public override void Interact()
    {
        used = true;
        Debug.Log($"{upgradeName} interact");
        upgradeStat.Invoke(upgradeName);
        Destroy(gameObject);

    }
}
