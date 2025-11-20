using Game399.Shared.Models;
using PlayerStuff;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private AudioClip startScreenTrack;
    [SerializeField] private AudioClip coffeeShopTrack;
    [SerializeField] private AudioClip combatTrack;
    public static StateManager Instance {get; private set;}
    public GameStates currentGameState = GameStates.MainMenu;
    public ShopStates currentShopState  = ShopStates.Transition;
    public ShopStates nextShopState = ShopStates.DayTime; 
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
                currentGameState = state;
                break;
            case GameStates.CoffeeShop:
                if(currentGameState == GameStates.Combat || currentGameState == GameStates.MainMenu)
                {
                    SoundManager.Instance.switchBackGroundTrackWithFade(coffeeShopTrack);
                }
                currentGameState = state;
                break;
            case GameStates.Combat:
                SoundManager.Instance.switchBackGroundTrackWithFade(combatTrack);
                currentGameState = state;
                break;
            case GameStates.Dialogue:
                currentGameState = state;
                break;
            case GameStates.PauseMenu:
                currentGameState = state;
                break;
            case GameStates.StatsMenu:
                currentGameState = state;
                break;
            case GameStates.InventoryMenu:
                currentGameState = state;
                break;
            case GameStates.MakingCoffee:
                currentGameState = state;
                break;
            case GameStates.TakingOutTrash:
                currentGameState = state;
                break;
        }
    }

    private void SwitchCoffeeShopState(ShopStates state)
    {
        switch(state)
        {
            case ShopStates.DayTime:
                currentShopState = state;
                nextShopState = ShopStates.NightTime;
                break;
            case ShopStates.NightTime:
                currentShopState = state;
                nextShopState = ShopStates.DayTime;
                break;
            case ShopStates.Transition:
                currentShopState = state;
                break;
            case ShopStates.OverTime:
                currentShopState = state;
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
    public void SwitchToMakingCofee()
    {
        SwitchState(GameStates.MakingCoffee);
    }

    public void SwitchToTakingOutTrash()
    {
        SwitchState(GameStates.TakingOutTrash);
    }

    public void SwitchShopToDay()
    {
        SwitchCoffeeShopState(ShopStates.DayTime);
    }
    public void SwitchShopToNight()
    {
        SwitchCoffeeShopState(ShopStates.NightTime);
    }
    public void SwitchShopToOverTime()
    {
        SwitchCoffeeShopState(ShopStates.OverTime);
    }

    public void SwitchShopToTransition()
    {
        SwitchCoffeeShopState(ShopStates.Transition);
    }
}
