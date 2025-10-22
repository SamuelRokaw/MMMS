using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CombatControl combatControl;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float dashMultiplier = 2f;
    [SerializeField] private float dashDuration = 1f;

    private Color originalColor = Color.white;
    private void Awake()
    {
        spCost = 2;
        
        if (combatControl == null)
        {
            combatControl = GetComponentInParent<CombatControl>();
        }
        if (playerSprite == null)
        {
            playerSprite = GetComponentInParent<SpriteRenderer>();
        }
    }
    
    public override void skillActivate()
    {
        StartCoroutine(DashCoroutine());
    }
    
    private IEnumerator DashCoroutine()
    {
        combatControl.spdMod = dashMultiplier;
        if (playerSprite != null)
        {
            playerSprite.color = Color.blue;
        }

        yield return new WaitForSeconds(dashDuration);
        combatControl.spdMod = 1f;
        if (playerSprite != null)
        {
            playerSprite.color = originalColor;
        }
    }
}
