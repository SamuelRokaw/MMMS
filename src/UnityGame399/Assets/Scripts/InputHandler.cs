using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class InputHandler : MonoBehaviour
{
    //we will have a seperate class that changes these
    public OverworldMovement owM; //overworld
    public OverworldInteraction owI; //overworld
    public CombatControl cC;  //combat
    public CanvasGroup pauseMenu; //pause menu
    public CanvasGroup statsMenu;
    public TextMeshProUGUI statsText;
    public Stats playerStats;
    public bool inCombat = false;
    public bool isPaused = false;
    public bool isStatsOpen = false;
    
    //action dictionary
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode skill1Key;
    public KeyCode skill2Key;
    public KeyCode attackKey;
    public KeyCode pauseKey;
    public KeyCode statsKey;
    private Dictionary<KeyCode, Action> inputDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputDictionary = new Dictionary<KeyCode, Action>
        {
            {upKey, moveup },
            {downKey, movedown},
            {leftKey, moveleft},
            {rightKey, moveright},
            {attackKey, attack},
            {skill1Key, skill1},
            {skill2Key, skill2},
            {pauseKey, pause},
            {statsKey, stats}
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses and call the corresponding method
        foreach (var entry in inputDictionary)
        {
            if (Input.GetKeyDown(entry.Key) && (entry.Key == KeyCode.Escape || entry.Key == KeyCode.Return)) // If the key is pressed down and it's the pause key
            {
                entry.Value.Invoke(); // Call the associated method
            }
            else if (Input.GetKey(entry.Key) && entry.Key != KeyCode.Escape && entry.Key != KeyCode.Return) // If the key is pressed
            {
                entry.Value.Invoke(); // Call the associated method
            }
        }
    }

    public void stats()
    {
        if (inCombat || isPaused)
        {
            return;
        }
        
        if (isStatsOpen)
        {
            isStatsOpen = false;
            statsMenu.alpha = 0;
            statsMenu.blocksRaycasts = false;
            statsMenu.interactable = false;
        }
        else
        {
            statsText.text = $"Level: {playerStats.Level}\nXP: {playerStats.Experience}\nXP needed:  {playerStats.ExperienceToNextLevel}\nHealth: {playerStats.MaxHealth}\nSP: {playerStats.CurrentSP}\nOxygen: {playerStats.MaxOxygen}\nAttack: {playerStats.AttackPower}";
            isStatsOpen = true;
            statsMenu.alpha = 1;
            statsMenu.blocksRaycasts = true;
            statsMenu.interactable = true;
        }
    }
    
    public void pause()
    {
        if (inCombat || isStatsOpen)
        {
            return;
        }
        
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.alpha = 0;
            pauseMenu.blocksRaycasts = false;
            pauseMenu.interactable = false;
        }
        else
        {
            isPaused = true;
            pauseMenu.alpha = 1;
            pauseMenu.blocksRaycasts = true;
            pauseMenu.interactable = true;
        }
    }
    
    
    private void moveup()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            cC.moveup();
        }
        else
        {
            owM.moveup();
        }
    }
    private void movedown()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            cC.movedown();
        }
        else
        {
            owM.movedown();
        }
    }
    private void moveleft()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            cC.moveleft();
        }
        else
        {
            owM.moveleft();
        }
    }
    private void moveright()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            cC.moveright();
        }
        else
        {
            owM.moveright();
        }
    }
    private void skill1()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            Debug.Log("trying to use skill1");
            cC.skill1();
        }
    }

    private void skill2()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            Debug.Log("trying to use skill2");
            cC.skill2();
        }
    }

    private void attack()
    {
        if (isPaused || isStatsOpen)
        {
            return;
        }
        if(inCombat)
        {
            Debug.Log("trying to attack");
            cC.punch();
        }
        else
        {
            Debug.Log("trying to interact");
            owI.Interact();
        }
    }
}
