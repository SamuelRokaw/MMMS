using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] private Stats stats;

    public void NewGame()
    {
        Logger.Instance.Info("Creating new game");
        PlayerPrefs.DeleteAll(); //deletes all saved data, player stat, area interactables data
        stats.ResetPlayerPrefs(); // makes a new player saved data to be loaded

    }

    public void LoadGame()
    {
        Logger.Instance.Info("Loading Saved Game");
    }

    
}
