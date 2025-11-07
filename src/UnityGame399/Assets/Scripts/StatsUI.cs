using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class StatsUI : MonoBehaviour
{
    public CanvasGroup statsMenu;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI coffeeText;
    //public Button[] skillButton;
    public Stats playerStats;
    //public SkillButtons skillButtons;
    public GameObject statsButtons;

    public void closeMenu()
    {
        statsMenu.alpha = 0;
        statsMenu.blocksRaycasts = false;
        statsMenu.interactable = false;
    }

    public void openMenu()
    {
        displayStatsText();
        displayCoffeeText();
        
        /*
         if (playerStats.HasDashSkill)
         
        {
            skillButton[0].interactable = true;
        }
        if (playerStats.HasSpearSkill)
        {
            skillButton[1].interactable = true;
        }
        if (playerStats.HasThreeSkill)
        {
            skillButton[2].interactable = true;
        }
        if (playerStats.HasFourSkill)
        {
            skillButton[3].interactable = true;
        }
        if (playerStats.HasFiveSkill)
        {
            skillButton[4].interactable = true;
        }
        if (playerStats.HasSixSkill)
        {
            skillButton[5].interactable = true;
        }
        skillButtons.OnOpen();
        */
        statsMenu.alpha = 1;
        statsMenu.blocksRaycasts = true;
        statsMenu.interactable = true;

        checkIfHasPoints();
    }

    public void increaseStat(int statToIncrease) // 1:hp, 2:sp, 3:luck, 4:attack
    {
        PlayerStatEvents.PlayerUpgradesStat.Invoke(statToIncrease);
        checkIfHasPoints();
        displayStatsText();
    }

    private void checkIfHasPoints()
    {
        if (playerStats.StatPoints > 0)
        {
            statsButtons.SetActive(true);
        }
        else
        {
            statsButtons.SetActive(false);
        }
    }

    private void displayStatsText()
    {
        statsText.text = $"Level: {playerStats.Level}\nXP: {playerStats.Experience}\nXP needed:  {playerStats.ExperienceToNextLevel}\nHealth: {playerStats.MaxHealth}\nSP: {playerStats.MaxSP}\nLuck: {playerStats.Luck}\nAttack: {playerStats.AttackPower}";
    }

    private void displayCoffeeText()
    {
        coffeeText.text = $"Money:  {playerStats.currentGold}\nCaf Beans: {playerStats.currentCafBean}\nDeCaf Beans: {playerStats.currentDecafBean}\nMilk Creamer: {playerStats.currentMilkCreamer}\nCaramel Creamer: {playerStats.currentCarCreamer}";
    }
}
