using UnityEngine;
using TMPro;

public class LevelGate : Interactable
{
    public int LevelRequirement;
    private Stats pStats;
    private void Awake()
    {
        used = false;
        pStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
    }
    public override void Interact()
    {
        if (pStats.Level >= LevelRequirement)
        {
            used = true;
            Debug.Log("level gate opened");
            gameObject.SetActive(false);
        }
        

    }
}
