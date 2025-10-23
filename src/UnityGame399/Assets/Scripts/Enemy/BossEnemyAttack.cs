using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAttack : AttackPattern
{
    public float chargeSpeed = 5f;
    public float chargeTime = 3f;
    public int chargeCount = 3;
    public float stunTime = 1f;
    public float delayBetweenCharges = 0.5f;
    public GameObject projectile;
    public List<Transform> spawnPoints;
    public BossEnemyMovement bem;
    
    private GameObject[] topWallNodes;
    private GameObject[] bottomWallNodes;
    private GameObject[] leftWallNodes;
    private GameObject[] rightWallNodes;
    
    private void Awake()
    {
        topWallNodes = GameObject.FindGameObjectsWithTag("TopWall");
        bottomWallNodes = GameObject.FindGameObjectsWithTag("BottomWall");
        leftWallNodes = GameObject.FindGameObjectsWithTag("LeftWall");
        rightWallNodes = GameObject.FindGameObjectsWithTag("RightWall");
    }
    
    public override void attack()
    {
        base.attack();
        StartCoroutine(MultiChargeSequence());
    }
    
    private IEnumerator MultiChargeSequence()
    {
        for (int i = 0; i < chargeCount; i++)
        {
            Transform targetNode = PickRandomNodeFromDifferentWall();
            
            if (targetNode != null)
            {
                yield return StartCoroutine(DashToNode(targetNode, i == chargeCount - 1));
            }
            
            if (i < chargeCount - 1)
            {
                yield return new WaitForSeconds(delayBetweenCharges);
            }
        }

        bem.spdMod = 0f;
        bem.rotspdMod = 0f;
        bem.SetTargetNode(null, "");
        yield return new WaitForSeconds(stunTime);
        bem.spdMod = 1f;
        bem.rotspdMod = 1f;
    }
    
    private Transform PickRandomNodeFromDifferentWall()
    {
        string lastWall = bem.GetLastWallTag();
        GameObject[] availableNodes = null;
        string newWallTag = "";
        
        int attempts = 0;
        while (attempts < 10)
        {
            int wallChoice = Random.Range(0, 4);
            
            switch (wallChoice)
            {
                case 0:
                    if (lastWall != "TopWall" && topWallNodes.Length > 0)
                    {
                        availableNodes = topWallNodes;
                        newWallTag = "TopWall";
                    }
                    break;
                case 1:
                    if (lastWall != "BottomWall" && bottomWallNodes.Length > 0)
                    {
                        availableNodes = bottomWallNodes;
                        newWallTag = "BottomWall";
                    }
                    break;
                case 2:
                    if (lastWall != "LeftWall" && leftWallNodes.Length > 0)
                    {
                        availableNodes = leftWallNodes;
                        newWallTag = "LeftWall";
                    }
                    break;
                case 3:
                    if (lastWall != "RightWall" && rightWallNodes.Length > 0)
                    {
                        availableNodes = rightWallNodes;
                        newWallTag = "RightWall";
                    }
                    break;
            }
            
            if (availableNodes != null) break;
            attempts++;
        }
        
        if (availableNodes != null && availableNodes.Length > 0)
        {
            GameObject chosenNode = availableNodes[Random.Range(0, availableNodes.Length)];
            bem.SetTargetNode(chosenNode.transform, newWallTag);
            return chosenNode.transform;
        }
        
        return null;
    }
    
    private IEnumerator DashToNode(Transform targetNode, bool isLastCharge)
    {
        bem.spdMod = chargeSpeed;
        
        float elapsedTime = 0f;
        while (!bem.HasReachedTarget() && elapsedTime < chargeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        if (!isLastCharge)
        {
            PerformNodeAction();
        }
        else
        {
            PerformFinalNodeAction();
        }
    }
    
    private void PerformNodeAction()
    {
        spawnBullets();
    }
    
    private void PerformFinalNodeAction()
    {
        Debug.Log("boss reached final node");
    }
    
    private void spawnBullets()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        }
    }
}