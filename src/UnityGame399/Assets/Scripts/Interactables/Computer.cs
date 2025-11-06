using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Computer : Interactable
{
    public GameObject computerUI;
    public CanvasGroup missionGroup;
    public List<GameObject> missions;
    public CanvasGroup shopGroup;
    public List<bool> upgrades;
    public bool currentTab = false; //false for missions, true for shop

    private void Awake()
    {
    }

    public override void Interact()
    {
        Logger.Instance.Info("Computer interact");
        OpenUI();
        
    }

    public void OpenUI()
    {
        computerUI.SetActive(true);
    }

    public void CloseUI()
    {
        computerUI.SetActive(false);
    }

    public void ChooseMission(int missionIndex)
    {
        Instantiate(missions[missionIndex]);
    }

    public void ChooseUpgrade(int shopIndex) //currently not used but is for future
    {
        //other stuff like decreasing money also goes here
        upgrades[shopIndex] = true;
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
        }
        else
        {
            Logger.Instance.Info("Switched to shop tab");
            DeactivateCG(missionGroup);
            ActivateCG(shopGroup);
            currentTab = true;
        }
    }
}
