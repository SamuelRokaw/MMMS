using System.Collections;
using UnityEngine;

public class SpawnEnemyOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private GameObject warningSymbol;
    [SerializeField] private int blinkCount = 3;
    [SerializeField] private float blinkSpeed = 0.2f;

    public void SpawnEnemy()
    {
        if (enemyToSpawn != null)
        {
            EnemySpawnManager manager = FindFirstObjectByType<EnemySpawnManager>();
            if (manager == null)
            {
                GameObject managerObj = new GameObject("EnemySpawnManager");
                manager = managerObj.AddComponent<EnemySpawnManager>();
            }
            
            manager.SpawnWithWarning(enemyToSpawn, warningSymbol, blinkCount, blinkSpeed);
        }
        else
        {
            Logger.Instance.Warn("This mob is meant to spawn an enemy, but nothing to spawn.");
        }
    }
}