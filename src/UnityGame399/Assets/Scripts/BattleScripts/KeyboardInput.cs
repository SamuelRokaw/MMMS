using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Diver Diver;

    private void Awake()
    {
        
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // move up
            Diver.MoveManually(new Vector2(0, 1));
        }
        if (Input.GetKey(KeyCode.S))
        {
            // move down
            Diver.MoveManually(new Vector2(0, -1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            // move left
            Diver.MoveManually(new Vector2(-1, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            // right
            Diver.MoveManually(new Vector2(1, 0));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Diver.Punch();
        }
    }
}
