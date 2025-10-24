using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using PlayerStuff;

public class CombatControl : MonoBehaviour
{
    // movement objects idk
    private Rigidbody2D cPlayerRB;
    private Transform cPlayerTran;
    private Stats stats;
    
    //punching
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
        cPlayerRB = GetComponent<Rigidbody2D>();
        cPlayerTran = cPlayerRB.transform;
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
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.up * spdMod * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void movedown()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.down * spdMod * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 180);
    }

    public void moveleft()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.left * spdMod * speed * Time.deltaTime);
        cPlayerTran.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void moveright()
    {
        zerovelocity();
        cPlayerRB.MovePosition(cPlayerRB.position + Vector2.right * spdMod * speed * Time.deltaTime);
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
        cPlayerRB.linearVelocity = Vector2.zero;
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
}
