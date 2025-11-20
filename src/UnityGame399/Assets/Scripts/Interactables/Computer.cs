using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Computer : Interactable
{
    public GameObject computerUI;
    public CanvasGroup missionGroup;
    public List<GameObject> missions;
    public CanvasGroup shopGroup;
    public bool currentTab = false; //false for missions, true for shop
    public Button missionButton;
    public Button shopButton;
    private Stats stats;
    
    //upgrades
    public List<Upgrades> upgrades;
    public List<int> upgradeCosts;
    public List<GameObject> upgradesUI;

    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
        missionButton.interactable = false;
        shopButton.interactable = true;
    }

    public override void Interact()
    {
        Logger.Instance.Info("Computer interact");
        OpenUI();
        
    }

    public void OpenUI()
    {
        Logger.Instance.Info("Computer UI opened");
        computerUI.SetActive(true);
    }

    public void CloseUI()
    {
        Logger.Instance.Info("Computer UI closed");
        computerUI.SetActive(false);
    }

    public void ChooseMission(int missionIndex)
    {
        Logger.Instance.Info("Mission chosen");
        CloseUI();
        CombatLoader.Instance.LoadCombat(missions[missionIndex]);
    }

    public void ChooseUpgrade(int shopIndex) //currently not used but is for future
    {
        Logger.Instance.Info("Upgrade chosen");
        if (upgradeCosts[shopIndex] <= stats.currentGold)
        {
            stats.ChangeGold(-1 * upgradeCosts[shopIndex]);
            upgrades[shopIndex].Unlock();
            upgradesUI[shopIndex].SetActive(false);
        }
        
    }
    
    public void ActivateCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void DeactivateCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void SwitchTab()
    {
        if (currentTab)
        {
            Logger.Instance.Info("Switched to mission tab");
            DeactivateCG(shopGroup);
            ActivateCG(missionGroup);
            currentTab = false;
            missionButton.interactable = false;
            shopButton.interactable = true;
        }
        else
        {
            Logger.Instance.Info("Switched to shop tab");
            DeactivateCG(missionGroup);
            ActivateCG(shopGroup);
            currentTab = true;
            shopButton.interactable = false;
            missionButton.interactable = true;
        }
    }
}
