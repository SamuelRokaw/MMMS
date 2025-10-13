using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CombatControl : MonoBehaviour
{
    // movement objects idk
    private Rigidbody2D cPlayerRB;
    private Transform cPlayerTran;
    
    //punching
    [SerializeField] private Transform punchSpawn;
    [SerializeField] private GameObject punchPrefab;
    private bool onCooldown;
    
    //skills
    //public Skill skill1;
    //public Skill skill2;
    
    //movement stats
    public float speed = 1f;
    public float spdMod = 1f; //for dash and similar abilities
    private void Awake()
    {
        cPlayerRB = GetComponent<Rigidbody2D>();
        cPlayerTran = cPlayerRB.transform;
    }

    public void moveup()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.up * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void movedown()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.down * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 180);
    }

    public void moveleft()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.left * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void moveright()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.right * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 270);
    }

    public void punch()
    {
        if(!onCooldown)
        {
            Instantiate(punchPrefab, punchSpawn.position, punchSpawn.rotation);
            StartCoroutine(Cooldown(0.25f));
        }
    }

    public void skill1()
    {
        Debug.Log("a skill would go here");
    }

    public void skill2()
    {
        Debug.Log("a 2nd skill would go here");
    }

    IEnumerator Cooldown(double amount)
    {
        double i = 0;
        onCooldown = true;
        while (i < amount)
        {
            yield return new WaitForSeconds(0.01f);
            i += 0.01;
        }
        onCooldown = false;
    }

    private void zerovelocity()
    {
        cPlayerRB.linearVelocity = Vector2.zero;
    }
    
}
