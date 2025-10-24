using UnityEngine;
using TMPro;

public class LevelGate : Interactable
{
    public int LevelRequirement;
    private Stats pStats;
    private void Awake()
    {
        if (used)
        {
            gameObject.SetActive(false);
        }
        pStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
    }
    public override void Interact()
    {
        if (pStats.Level >= LevelRequirement)
        {
            used = true;
            Logger.Instance.Info("level gate opened");
            gameObject.SetActive(false);
        }
        

    }
}
