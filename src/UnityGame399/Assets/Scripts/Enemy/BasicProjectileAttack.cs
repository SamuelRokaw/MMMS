using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class BasicProjectileAttack : AttackPattern
{
    public GameObject projectile;
    public List<Transform> spawnPoints;
    public AudioClip shootSFX;
    public override void attack()
    {
        base.attack();
        spawnBullets();
    }
    

    private void spawnBullets()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if(shootSFX != null)
            {
                SoundManager.Instance.playEnemySFX(shootSFX);
            }
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            
        }
    }
}
