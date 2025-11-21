using UnityEngine;
using PlayerStuff;
using System.Collections.Generic;

public class CoffeeShop : MonoBehaviour
{
    //nextstate should only ever be night/day, stored as int in ShopData 0 for day and 1 for night
    public List<Upgrades> upgrades; //list of bools for whetehr upgrades have been purchased
    private void Awake()
    {
        LoadFromPlayerPrefs();
    }
    
    public void SaveToPlayerPrefs()
    {
        if(StateManager.Instance.currentShopState == ShopStates.Transition)
        {
            ShopData data = Save();
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("CoffeeShop", json);
            PlayerPrefs.Save();
        }
        else
        {
            Logger.Instance.Info("CoffeeShop Not Saved because it is not transition state");
        }
    }

    public void LoadFromPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("CoffeeShop")) return;
        string json = PlayerPrefs.GetString("CoffeeShop");
        ShopData data = JsonUtility.FromJson<ShopData>(json);
        Load(data);
    }
    public void Load(ShopData data)
    {
        switch (data.nextState)
        {
            case 0:
                StateManager.Instance.nextShopState = ShopStates.DayTime;
                StateManager.Instance.currentShopState = ShopStates.Transition;
                break;
            case 1:
                StateManager.Instance.nextShopState = ShopStates.NightTime;
                StateManager.Instance.currentShopState = ShopStates.Transition;
                break;
            default:
                StateManager.Instance.nextShopState = ShopStates.DayTime;
                StateManager.Instance.currentShopState = ShopStates.Transition;
                break;
        }
        int index = 0;
        foreach (char c in data.upgradeStatus)
        {
            if (c == '0')
            {
                upgrades[index].unlocked = false;
            }
            else
            {
                upgrades[index].unlocked = true;
            }
            index++;
        }
    }
    public ShopData Save()
    {
        int tempint = 0;
        switch (StateManager.Instance.nextShopState)
        {
            case ShopStates.DayTime:
                tempint = 0;
                break;
            case ShopStates.NightTime:
                tempint = 1;
                break;
            default:
                tempint = 1;
                break;
        }
        string tempstring = "";
        foreach (bool i in upgrades)
        {
            if (!i)
            {
                tempstring += '0';
            }
            else
            {
                tempstring += '1';
            }
        }
        return new ShopData
        {
            upgradeStatus = tempstring,
            nextState = tempint,
        };
    }
}
