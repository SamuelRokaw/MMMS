using UnityEngine;
using System;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    //we will have a seperate class that changes these
    public OverworldMovement owM; 
    //public CombatControl cC; 
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
            Debug.Log("using attack");
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
            Debug.Log("using attack");
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
            Debug.Log("using attack");
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
            Debug.Log("using attack");
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
            Debug.Log("using skill1");
        }
    }

    private void skill2()
    {
        if(inCombat)
        {
            Debug.Log("using skill2");
        }
    }

    private void attack()
    {
        if(inCombat)
        {
            Debug.Log("using attack");
        }
        else
        {
            Debug.Log("interacting");
        }
    }
}
