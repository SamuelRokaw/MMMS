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
    public Button[] skillButton;
    public Stats playerStats;
    public SkillButtons skillButtons;

    public void closeMenu()
    {
        statsMenu.alpha = 0;
        statsMenu.blocksRaycasts = false;
        statsMenu.interactable = false;
    }

    public void openMenu()
    {
        statsText.text = $"Level: {playerStats.Level}\nXP: {playerStats.Experience}\nXP needed:  {playerStats.ExperienceToNextLevel}\nHealth: {playerStats.MaxHealth}\nSP: {playerStats.CurrentSP}\nOxygen: {playerStats.MaxOxygen}\nAttack: {playerStats.AttackPower}";
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
        statsMenu.alpha = 1;
        statsMenu.blocksRaycasts = true;
        statsMenu.interactable = true;
    }
}
