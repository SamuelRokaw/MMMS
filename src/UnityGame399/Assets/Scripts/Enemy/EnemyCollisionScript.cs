using System;
using UnityEngine;
using System.Collections;
using PlayerStuff;

public class EnemyCollisionScript : MonoBehaviour
{
    public BasicEnemy be;
    [SerializeField] private bool onCooldown = false;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private bool takeDamageOnCooldown = false;
    [SerializeField] private float damageCoolDown = 0.1f;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && !takeDamageOnCooldown)
        {
            Debug.Log("Fish Takes Damage");
            int amount = collision.gameObject.GetComponent<PunchLogic>().aP;
            be.changeHealth(amount);
            StartCoroutine(TakeDamageCoolDown());
        }
        if (collision.gameObject.CompareTag("CombatPlayer") && !onCooldown)
        {
            PlayerStatEvents.PlayerTakesDamage(1);
            StartCoroutine(Cooldown());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Punch") && !takeDamageOnCooldown)
        {
            Debug.Log("Fish Takes Damage");
            int amount = other.gameObject.GetComponent<PlayerBullet>().aP;
            be.changeHealth(amount);
            StartCoroutine(TakeDamageCoolDown());
        }
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(attackCoolDown);
        onCooldown = false;
    }

    IEnumerator TakeDamageCoolDown()
    {
        takeDamageOnCooldown = true;
        yield return new WaitForSeconds(damageCoolDown);
        takeDamageOnCooldown = false;
    }
}
