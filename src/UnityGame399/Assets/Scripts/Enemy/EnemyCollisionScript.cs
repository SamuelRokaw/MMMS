using UnityEngine;
using System.Collections;

public class EnemyCollisionScript : MonoBehaviour
{
    public BasicEnemy be;
    public bool onCooldown = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            Debug.Log("Fish Takes Damage");
            int amount = collision.gameObject.GetComponent<PunchLogic>().aP;
            be.changeHealth(amount);
        }
        if (collision.gameObject.CompareTag("CombatPlayer") && !onCooldown)
        {
            PlayerStatEvents.PlayerTakesDamage(1);
            StartCoroutine(Cooldown());
        }
        
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }
}
