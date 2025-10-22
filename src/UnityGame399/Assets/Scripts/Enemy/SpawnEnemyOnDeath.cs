using UnityEngine;

public class SpawnEnemyOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;

    public void SpawnEnemy()
    {
        if (enemyToSpawn != null)
        {
            enemyToSpawn.SetActive(true);
        }
        else
        {
            Debug.LogWarning("This mob is meant to spawn an enemy, but nothing to spawn.");
        }
    }
}
