using System;
using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //Enemy Stats
    public int maxHealth = 1;
    public int currentHealth;
    public float atkCooldown = 9f; //9 for charge, 4 for projectile based attacks
    public bool attacking = false;
    public AttackPattern ap;
    [SerializeField] private bool isBoss = false;
    public static Action benemyDied;
    
    // death effects
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private ParticleSystem bloodSplatterPrefab;
    //player location
    public Transform player;

    void FixedUpdate()
    {
        if (!attacking && ap!=null)
        {
            StartCoroutine(attackCooldown());
        }
        
    }

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("CombatPlayer").transform;
        currentHealth = maxHealth;
        attacking = false;
    }
    
    
    private void die()
    {
        benemyDied.Invoke();
        playDeathEffects();
        enemySpawnsEnemy();
        Destroy(gameObject);
        if (isBoss)
        {
            BossAquarium.bossDead.Invoke();
        }
    }

    private void enemySpawnsEnemy()
    {
        SpawnEnemyOnDeath spawner = GetComponent<SpawnEnemyOnDeath>();
        if (spawner != null)
        {
            spawner.SpawnEnemy();
        }
    }

    private void playDeathEffects()
    {
        AudioClip soundToPlay = deathSound != null ? deathSound : EnemyDeathEffectsManager.Instance?.defaultDeathSound;
        if (soundToPlay != null)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, transform.position, 0.3f);
        }
        
        ParticleSystem particleToSpawn = bloodSplatterPrefab != null ? bloodSplatterPrefab : EnemyDeathEffectsManager.Instance?.defaultBloodSplatter;
        if (particleToSpawn != null)
        {
            ParticleSystem splatter = Instantiate(particleToSpawn, transform.position, Quaternion.identity);
            Destroy(splatter.gameObject, 0.2f);
        }
    }

    public void changeHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    private IEnumerator attackCooldown()
    {
        attacking = true;
        yield return new WaitForSeconds(1f);
        ap.attack();
        yield return new WaitForSeconds(atkCooldown - 1);
        attacking = false;
        
    }
    
}
