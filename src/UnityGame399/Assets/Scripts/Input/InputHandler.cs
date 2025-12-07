using UnityEngine;
using System;
using PlayerStuff;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class InputHandler : MonoBehaviour
{
    public OverworldMovement owM;
    public OverworldInteraction owI;
    public CombatControl cC;
    public CanvasGroup pauseMenu;
    public StatsUI statsUI;
    public InventorySlidePanel inventorySlidePanel;
    public GameObject ticketUI;
    public bool isTicketUIOpen = true;

    [SerializeField] private List<Sprite> playerSprites;
    [SerializeField] private SpriteRenderer owSpriteRenderer;

    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction interactAction;
    private InputAction skill1Action;
    private InputAction skill2Action;
    private InputAction pauseAction;
    private InputAction statsAction;
    private InputAction inventoryAction;
    private InputAction ticketAction;

    private void OnEnable()
    {
        // Get the Player action map from your singleton
        actionMap = PlayerControlsManager.Instance.inputActions.FindActionMap("Player");

        // Cache actions
        moveAction = actionMap.FindAction("Move");
        attackAction = actionMap.FindAction("Attack");
        interactAction = actionMap.FindAction("Interact");
        skill1Action = actionMap.FindAction("Skill1");
        skill2Action = actionMap.FindAction("Skill2");
        pauseAction = actionMap.FindAction("Pause");
        statsAction = actionMap.FindAction("Stats");
        inventoryAction = actionMap.FindAction("Inventory");
        ticketAction = actionMap.FindAction("Ticket");

        // Subscribe to events with named methods
        attackAction.performed += OnAttack;
        interactAction.performed += OnInteract;
        skill1Action.performed += OnSkill1;
        skill2Action.performed += OnSkill2;
        pauseAction.performed += OnPause;
        statsAction.performed += OnStats;
        inventoryAction.performed += OnInventory;
        ticketAction.performed += OnTicket;

        actionMap.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        attackAction.performed -= OnAttack;
        interactAction.performed -= OnInteract;
        skill1Action.performed -= OnSkill1;
        skill2Action.performed -= OnSkill2;
        pauseAction.performed -= OnPause;
        statsAction.performed -= OnStats;
        inventoryAction.performed -= OnInventory;
        ticketAction.performed -= OnTicket;

        actionMap.Disable();
    }
    
    // Named handlers for input actions
    private void OnStats(InputAction.CallbackContext ctx) => stats();
    private void OnInventory(InputAction.CallbackContext ctx) => inventory();
    private void OnPause(InputAction.CallbackContext ctx) => pause();
    private void OnSkill1(InputAction.CallbackContext ctx) => skill1();
    private void OnSkill2(InputAction.CallbackContext ctx) => skill2();
    private void OnAttack(InputAction.CallbackContext ctx) => attack();
    private void OnInteract(InputAction.CallbackContext ctx) => Interact();
    private void OnTicket(InputAction.CallbackContext ctx) => ToggleTicket();

    private void Update()
    {
        // Poll movement every frame
        Vector2 input = moveAction.ReadValue<Vector2>();
        HandleMove(input);
    }

    private void HandleMove(Vector2 input)
    {
        if (input == Vector2.zero) return;

        if (StateManager.Instance.currentGameState == GameStates.Combat)
        {
            if (input.y > 0)
            {
                cC.moveup(); 
                cC.changeSprite(playerSprites[1], 1);
            }
            else if (input.y < 0)
            {
                cC.movedown(); 
                cC.changeSprite(playerSprites[0], 4);
            }
            else if (input.x < 0)
            {
                cC.moveleft(); 
                cC.changeSprite(playerSprites[2], 3);
            }
            else if (input.x > 0)
            {
                cC.moveright(); 
                cC.changeSprite(playerSprites[2], 2);
            }
        }
        else if (StateManager.Instance.currentGameState == GameStates.CoffeeShop || StateManager.Instance.currentGameState == GameStates.TakingOutTrash)
        {
            if (input.y > 0)
            {
                owSpriteRenderer.sprite = playerSprites[1]; owSpriteRenderer.flipX = false; 
                owM.moveup();
            }
            else if (input.y < 0)
            {
                owSpriteRenderer.sprite = playerSprites[0]; owSpriteRenderer.flipX = false; 
                owM.movedown();
            }
            else if (input.x < 0)
            {
                owSpriteRenderer.sprite = playerSprites[2]; owSpriteRenderer.flipX = true; 
                owM.moveleft();
            }
            else if (input.x > 0)
            {
                owSpriteRenderer.sprite = playerSprites[2]; owSpriteRenderer.flipX = false; 
                owM.moveright();
            }
        }
    }

    public void stats()
    {
        if (StateManager.Instance.currentGameState == GameStates.StatsMenu)
        {
            Logger.Instance.Info("Close Stats");
            statsUI.closeMenu();
            StateManager.Instance.SwitchToCoffeeShop();
            
        }
        else if(StateManager.Instance.currentGameState == GameStates.CoffeeShop )
        {
            Logger.Instance.Info("Open Stats");
            statsUI.openMenu();
            StateManager.Instance.SwitchToStats();
        }
    }
    
    public void inventory()
    {
        if(StateManager.Instance.currentGameState == GameStates.CoffeeShop && StateManager.Instance.nextShopState == ShopStates.NightTime)
        {
            inventorySlidePanel.TogglePanel();
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
    }

    private void Interact()
    {
        if (StateManager.Instance.currentGameState == GameStates.CoffeeShop || StateManager.Instance.currentGameState == GameStates.TakingOutTrash)
        {
            Logger.Instance.Info("trying to interact");
            owI.Interact();
        }
    }

    private void ToggleTicket()
    {
        if (StateManager.Instance.currentGameState == GameStates.CoffeeShop)
        {
            if (isTicketUIOpen)
            {
                ticketUI.SetActive(false);
                isTicketUIOpen = false;
            }
            else
            {
                ticketUI.SetActive(true);
                isTicketUIOpen = true;
            }
        }
    }
}
