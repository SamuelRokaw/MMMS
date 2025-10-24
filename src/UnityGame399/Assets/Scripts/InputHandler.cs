using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InputHandler : MonoBehaviour
{
    //we will have a seperate class that changes these
    public OverworldMovement owM; //overworld
    public OverworldInteraction owI; //overworld
    public CombatControl cC;  //combat
    public CanvasGroup pauseMenu; //pause menu
    public StatsUI statsUI;
    public bool inCombat = false;
    public bool isPaused = false;
    public bool isStatsOpen = false;
    public bool inDialogue = false;
    
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
    private Dictionary<KeyCode, Action> inputMoveDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputDictionary = new Dictionary<KeyCode, Action>
        {
            {attackKey, attack},
            {skill1Key, skill1},
            {skill2Key, skill2},
            {pauseKey, pause},
            {statsKey, stats}
        };
        inputMoveDictionary = new Dictionary<KeyCode, Action>
        {
            {upKey, moveup },
            {downKey, movedown},
            {leftKey, moveleft},
            {rightKey, moveright},
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses and call the corresponding method
        foreach (var entry in inputDictionary)
        {
            if (Input.GetKeyDown(entry.Key)) // If the key is pressed down
            {
                entry.Value.Invoke(); // Call the associated method
            }
            
        }
    }

    void FixedUpdate()  //for movement, should hopefully make it  consistent across all users
    {
        // Check for key presses and call the corresponding method
        foreach (var entry in inputMoveDictionary)
        {
            if (Input.GetKey(entry.Key)) // If the key is pressed
            {
                entry.Value.Invoke(); // Call the associated method
            }
            
        }
    }

    public void stats()
    {
        if (inCombat || isPaused || inDialogue)
        {
            return;
        }
        
        if (isStatsOpen)
        {
            Logger.Instance.Info("Close Stats");
            isStatsOpen = false;
            statsUI.closeMenu();
        }
        else
        {
            Logger.Instance.Info("Open Stats");
            statsUI.openMenu();
            isStatsOpen = true;
        }
    }
    
    public void pause()
    {
        if (inCombat || isStatsOpen || inDialogue)
        {
            return;
        }
        
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.alpha = 0;
            pauseMenu.blocksRaycasts = false;
            pauseMenu.interactable = false;
            Logger.Instance.Info("Close Pause Menu");
        }
        else
        {
            isPaused = true;
            pauseMenu.alpha = 1;
            pauseMenu.blocksRaycasts = true;
            pauseMenu.interactable = true;
            Logger.Instance.Info("Opened Pause Menu");
        }
    }
    
    
    private void moveup()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            cC.moveup();
            Logger.Instance.Info("Moved Up");
        }
        else
        {
            Logger.Instance.Info("Moved Up");
            owM.moveup();
        }
    }
    private void movedown()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("Moved Down");
            cC.movedown();
        }
        else
        {
            Logger.Instance.Info("Moved Down");
            owM.movedown();
        }
    }
    private void moveleft()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("Moved Left");
            cC.moveleft();
        }
        else
        {
            Logger.Instance.Info("Moved Right");
            owM.moveleft();
        }
    }
    private void moveright()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("Moved Right");
            cC.moveright();
        }
        else
        {
            Logger.Instance.Info("Moved Right");
            owM.moveright();
        }
    }
    private void skill1()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("trying to use skill1");
            cC.skill1();
        }
    }

    private void skill2()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("trying to use skill2");
            cC.skill2();
        }
    }

    private void attack()
    {
        if (isPaused || isStatsOpen || inDialogue)
        {
            return;
        }
        if(inCombat)
        {
            Logger.Instance.Info("trying to attack");
            cC.punch();
        }
        else
        {
            Logger.Instance.Info("trying to interact");
            owI.Interact();
        }
    }
}
