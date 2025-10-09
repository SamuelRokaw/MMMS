using UnityEngine;
using System;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    //we will have a seperate class that changes these
    public OverworldMovement owM; //overworld
    public OverworldInteraction owI; //overworld
    public CombatControl cC;  //combat
    public bool inCombat = false;
    
    //action dictionary
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode skill1Key;
    public KeyCode skill2Key;
    public KeyCode attackKey;
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
            {skill2Key, skill2}
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses and call the corresponding method
        foreach (var entry in inputDictionary)
        {
            if (Input.GetKey(entry.Key)) // If the key is pressed
            {
                entry.Value.Invoke(); // Call the associated method
            }
        }
    }
    
    private void moveup()
    {
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
        if(inCombat)
        {
            Debug.Log("trying to use skill1");
            cC.skill1();
        }
    }

    private void skill2()
    {
        if(inCombat)
        {
            Debug.Log("trying to use skill2");
            cC.skill2();
        }
    }

    private void attack()
    {
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
