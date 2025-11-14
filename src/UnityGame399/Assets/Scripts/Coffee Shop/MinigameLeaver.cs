using UnityEngine;
using UnityEngine.UIElements;

public class MinigameLeaver : MonoBehaviour
{
    [SerializeField] private Button leaveButton;
    [SerializeField] private CoffeeGame currentGame;
    [SerializeField] private GameObject currentMinigame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void EndMiniGame()
    {
        Logger.Instance.Info("Exiting minigame");
        currentGame.resetUseState();
        CoffeeGameLoader.Instance.UnloadMinigame(currentMinigame);
        StateManager.Instance.SwitchToCoffeeShop();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
