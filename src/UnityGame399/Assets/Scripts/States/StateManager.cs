using PlayerStuff;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private AudioClip startScreenTrack;
    [SerializeField] private AudioClip coffeeShopTrack;
    [SerializeField] private AudioClip combatTrack;
    public static StateManager Instance {get; private set;}
    public GameStates currentGameState = GameStates.MainMenu;
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.playBackGroundTrack(startScreenTrack);
    }

    private void SwitchState(GameStates state)
    {
        switch(state)
        {
            case GameStates.MainMenu:
                SoundManager.Instance.switchBackGroundTrackWithFade(startScreenTrack);
                break;
            case GameStates.CoffeeShop:
                SoundManager.Instance.switchBackGroundTrackWithFade(coffeeShopTrack);
                break;
            case GameStates.Combat:
                SoundManager.Instance.switchBackGroundTrackWithFade(combatTrack);
                break;
            case GameStates.Dialogue:
                break;
            case GameStates.PauseMenu:
                break;
            case GameStates.StatsMenu:
                break;
            case GameStates.InventoryMenu:
                break;
        }
    }

    public void SwitchToMain()
    {
        SwitchState(GameStates.MainMenu);
    }
    public void SwitchToCoffeeShop()
    {
        SwitchState(GameStates.CoffeeShop);
    }
    public void SwitchToCombat()
    {
        SwitchState(GameStates.Combat);
    }
    public void SwitchToStats()
    {
        SwitchState(GameStates.StatsMenu);
    }
    public void SwitchToDialogue()
    {
        SwitchState(GameStates.Dialogue);
    }
    public void SwitchToPause()
    {
        SwitchState(GameStates.PauseMenu);
    }
    public void SwitchToInventory()
    {
        SwitchState(GameStates.InventoryMenu);
    }
}
