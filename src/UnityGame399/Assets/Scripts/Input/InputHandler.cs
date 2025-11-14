using UnityEngine;
using System;
using System.Collections.Generic;
using PlayerStuff;
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
    public InventorySlidePanel inventorySlidePanel;
    
    //sprites
    [SerializeField] private List<Sprite> playerSprites;
    [SerializeField] private SpriteRenderer owSpriteRenderer;
    
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
    public KeyCode inventoryKey;
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
            {statsKey, stats},
            {inventoryKey, inventory}
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
        if (StateManager.Instance.currentGameState == GameStates.StatsMenu)
        {
            Logger.Instance.Info("Close Stats");
            StateManager.Instance.SwitchToCoffeeShop();
            statsUI.closeMenu();
        }
        else if(StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("Open Stats");
            statsUI.openMenu();
            StateManager.Instance.SwitchToStats();
        }
    }
    
    public void inventory()
    {
        if (StateManager.Instance.currentGameState == GameStates.InventoryMenu)
        {
            StateManager.Instance.SwitchToCoffeeShop();
            inventorySlidePanel.TogglePanel();
            Logger.Instance.Info("Close Coffee Inventory");
        }
        else if(StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            StateManager.Instance.SwitchToInventory();
            inventorySlidePanel.TogglePanel();
            Logger.Instance.Info("Opened Coffee Inventory");
        }
    }
    
    public void pause()
    {
        if (StateManager.Instance.currentGameState == GameStates.PauseMenu)
        {
            StateManager.Instance.SwitchToCoffeeShop();
            pauseMenu.alpha = 0;
            pauseMenu.blocksRaycasts = false;
            pauseMenu.interactable = false;
            Logger.Instance.Info("Close Pause Menu");
        }
        else if(StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            StateManager.Instance.SwitchToPause();
            pauseMenu.alpha = 1;
            pauseMenu.blocksRaycasts = true;
            pauseMenu.interactable = true;
            Logger.Instance.Info("Opened Pause Menu");
        }
    }
    
    
    private void moveup()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            cC.moveup();
            cC.changeSprite(playerSprites[1], 1);
            Logger.Instance.Info("Moved Up");
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("Moved Up");
            owSpriteRenderer.sprite = playerSprites[1];
            owSpriteRenderer.flipX = false;
            owM.moveup();
        }
    }
    private void movedown()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("Moved Down");
            cC.changeSprite(playerSprites[0], 4);
            
            cC.movedown();
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("Moved Down");
            owSpriteRenderer.sprite = playerSprites[0];
            owSpriteRenderer.flipX = false;
            owM.movedown();
        }
    }
    private void moveleft()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("Moved Left");
            cC.changeSprite(playerSprites[2], 3);
            
            cC.moveleft();
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("Moved Left");
            owSpriteRenderer.sprite = playerSprites[2];
            owSpriteRenderer.flipX = true;
            owM.moveleft();
        }
    }
    private void moveright()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("Moved Right");
            cC.changeSprite(playerSprites[2], 2);
            cC.moveright();
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("Moved Right");
            owSpriteRenderer.sprite = playerSprites[2];
            owSpriteRenderer.flipX = false;
            owM.moveright();
        }
    }
    private void skill1()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("trying to use skill1");
            cC.skill1();
        }
    }

    private void skill2()
    {
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("trying to use skill2");
            cC.skill2();
        }
    }

    private void attack()
    {
        
        if(StateManager.Instance.currentGameState == GameStates.Combat)
        {
            Logger.Instance.Info("trying to attack");
            cC.punch();
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            Logger.Instance.Info("trying to interact");
            owI.Interact();
        }
    }
}
