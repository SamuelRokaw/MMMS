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

    public static Action benemyDied;
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
        Destroy(gameObject);
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
