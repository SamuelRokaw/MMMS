using System;
using UnityEngine;
using UnityEngine.UI;

public class Diver : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SpriteRenderer DiverSpriteRenderer;
    private Vector2 lastFacingDirection = Vector2.down;

    public GameObject FistPrefab;
    private float PunchDistance = 1f;
    
    private float PunchCooldown = 0.5f;
    private float LastPunchTime = -1.0f;
    
    void Update()
    {
        
    }
    

    public void MoveManually(Vector2 direction)
    {
        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            lastFacingDirection = DirectionSnapper(direction);
            FaceCorrectDirection(direction);
        
            float xAmount = moveSpeed * direction.x * Time.deltaTime; 
            float yAmount = moveSpeed * direction.y * Time.deltaTime;

            Vector2 moveAmount = new Vector2(xAmount, yAmount);
        
            DiverSpriteRenderer.transform.Translate(moveAmount);

            KeepOnScreen();
        }
    }

    public void Punch()
    {
        if (Time.time < PunchCooldown + LastPunchTime)
        {
            return;
        }

        LastPunchTime = Time.time;
        
        Vector2 spawnPosition = getSpawnPositioning();

        GameObject hurtbox = Instantiate(FistPrefab, spawnPosition, Quaternion.identity);

        PunchLogic decay = hurtbox.GetComponent<PunchLogic>();
        if (decay != null)
        {
            decay.Initialize(lastFacingDirection);
        }
        
        SpriteRenderer hurtboxSR = hurtbox.GetComponent<SpriteRenderer>();
        if (hurtboxSR != null)
        {
            if (Mathf.Abs(lastFacingDirection.x) > Mathf.Abs(lastFacingDirection.y))
            {
                hurtboxSR.flipX = lastFacingDirection.x < 0;
                hurtboxSR.flipY = false;
            }
            else
            {
                hurtboxSR.flipY = lastFacingDirection.y < 0;
                hurtboxSR.flipX = false;
            }
        }
    }

    private Vector2 getSpawnPositioning()
    {
        return (Vector2)DiverSpriteRenderer.transform.position + (lastFacingDirection.normalized * PunchDistance);
    }
    
    private void KeepOnScreen()
    { 
       DiverSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(DiverSpriteRenderer);
    }
    
    private void FaceCorrectDirection(Vector2 direction)
    {
        // if move right
        if (direction.x > 0)
        {
            DiverSpriteRenderer.flipX = false;
        }
        // if nove left
        else if (direction.x < 0)
        {
            DiverSpriteRenderer.flipX = true;
        }
        if (direction.y > 0)
        {
            DiverSpriteRenderer.flipY = false;
        }
        // if nove left
        else if (direction.y < 0)
        {
            DiverSpriteRenderer.flipY = true;
        }
    }

    private Vector2 DirectionSnapper(Vector2 input)
    {
        if (input == Vector2.zero)
        {
            return lastFacingDirection;
        }

        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            (Vector2.up + Vector2.left).normalized,
            (Vector2.up + Vector2.right).normalized,
            (Vector2.down + Vector2.left).normalized,
            (Vector2.down + Vector2.right).normalized,
        };
        
        Vector2 bestMatch = directions[0];
        float maxDot = -Mathf.Infinity;

        foreach (var dir in directions)
        {
            float dot = Vector2.Dot(input.normalized, dir);
            if (dot > maxDot)
            {
                maxDot = dot;
                bestMatch = dir;
            }
        }

        return bestMatch;
    }
}