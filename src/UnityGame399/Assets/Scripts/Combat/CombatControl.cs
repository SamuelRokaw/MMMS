using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using PlayerStuff;

public class CombatControl : MonoBehaviour
{
    // movement objects idk
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField]private Transform cPlayerTran;
    private Stats stats;
    [SerializeField] private SpriteRenderer playerSprite;
    //punching
    [SerializeField] private List<Transform> punchSpawns;
    [SerializeField] private Transform punchSpawn;
    [SerializeField] private GameObject punchPrefab;
    private bool onCooldown;
    
    //skills
    public Dictionary<SkillTypes, Skill> skills =  new Dictionary<SkillTypes, Skill>();
    public Skill dash;
    public Skill spear;
    
    
    //skills to be called
    private Skill firstSkill = null;
    private Skill secondSkill = null;
    
    //movement stats
    public float speed = 1f;
    public float spdMod = 1f; //for dash and similar abilities
    private void Awake()
    {
        if (playerRB == null)
        {
            playerRB = GetComponent<Rigidbody2D>(); 
        }
        cPlayerTran = playerRB.transform;
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
        fillSkillDict();
        if (stats.SkillOne != SkillTypes.None)
        {
            firstSkill = skills[stats.SkillOne];
        }
        if (stats.SkillTwo != SkillTypes.None)
        {
            secondSkill = skills[stats.SkillTwo];
        }
    }

    public void moveup()
    {
        zerovelocity();
        punchSpawn = punchSpawns[0];
        playerRB.MovePosition(playerRB.position + Vector2.up * spdMod * speed * Time.deltaTime);
    }

    public void movedown()
    {
        zerovelocity();
        punchSpawn = punchSpawns[1];
        playerRB.MovePosition(playerRB.position + Vector2.down * spdMod * speed * Time.deltaTime);
    }

    public void moveleft()
    {
        zerovelocity();
        punchSpawn = punchSpawns[2];
        playerRB.MovePosition(playerRB.position + Vector2.left * spdMod * speed * Time.deltaTime);
    }

    public void moveright()
    {
        zerovelocity();
        punchSpawn = punchSpawns[3];
        playerRB.MovePosition(playerRB.position + Vector2.right * spdMod * speed * Time.deltaTime);
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
        if(firstSkill != null)
        {
            Logger.Instance.Info($"{stats.SkillOne} used");
            firstSkill.skillActivate(stats.CurrentSP);
        }
        else
        {
            Logger.Instance.Info("no skill to use");
        }

    }

    public void skill2()
    {
        if(secondSkill != null)
        {
            Logger.Instance.Info($"{stats.SkillTwo} used");
            secondSkill.skillActivate(stats.CurrentSP);
        }
        else
        {
            Logger.Instance.Info("no skill to use");
        }
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
        playerRB.linearVelocity = Vector2.zero;
    }

    private void fillSkillDict()
    {
        if (dash != null)
        {
            skills.Add(SkillTypes.Dash, dash);
        }
        if (spear != null)
        {
            skills.Add(SkillTypes.Spear, spear);
        }
        
    }

    public void changeSprite(Sprite sprite, int direction)
    {
        if (direction != 3)
        {
            playerSprite.flipX = false;
        }
        else
        {
            playerSprite.flipX = true;
        }
        playerSprite.sprite  = sprite;
    }
}
