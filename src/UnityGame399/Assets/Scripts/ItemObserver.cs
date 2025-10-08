using UnityEngine;

public class ItemObserver : MonoBehaviour
{

    public void OnEnable()
    {
        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        UpgradeItem.upgradeStat += OnItemInteracted;
    }

    private void Unsubscribe()
    {
        UpgradeItem.upgradeStat -= OnItemInteracted;
    }

    private void OnItemInteracted(string upgradeName)
    {
        Debug.Log("Item interaction observed");
        Debug.Log($"{upgradeName} increased");
        
    }
}
