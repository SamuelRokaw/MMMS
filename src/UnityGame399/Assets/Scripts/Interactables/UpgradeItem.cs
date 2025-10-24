using Unity.VisualScripting;
using UnityEngine;
using System;

public class UpgradeItem : Interactable
{
    public string upgradeName;
    public static Action<string> upgradeStat;

    private void Start()
    {
        if (used)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        used = true;
        Logger.Instance.Info($"{upgradeName} interact");
        upgradeStat.Invoke(upgradeName);
        gameObject.SetActive(false);
    }
}
