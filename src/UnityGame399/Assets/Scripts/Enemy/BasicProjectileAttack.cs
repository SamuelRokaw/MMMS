using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class BasicProjectileAttack : AttackPattern
{
    public GameObject projectile;
    public List<Transform> spawnPoints;
    private SoundManager sM;
    public override void attack()
    {
        base.attack();
        spawnBullets();
    }

    public void Start()
    {
        sM = GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundManager>();
    }

    private void spawnBullets()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            sM.playFSE();
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            
        }
    }
}
