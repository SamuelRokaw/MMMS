using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public void SpawnWithWarning(GameObject enemy, GameObject warning, int blinks, float speed)
    {
        StartCoroutine(SpawnEnemyWithWarning(enemy, warning, blinks, speed));
    }

    private IEnumerator SpawnEnemyWithWarning(GameObject enemy, GameObject warning, int blinks, float speed)
    {
        if (warning != null)
        {
            warning.transform.position = enemy.transform.position;

            for (int i = 0; i < blinks; i++)
            {
                warning.SetActive(true);
                yield return new WaitForSeconds(speed);
                warning.SetActive(false);
                yield return new WaitForSeconds(speed);
            }
        }
        
        enemy.SetActive(true);
    }
}