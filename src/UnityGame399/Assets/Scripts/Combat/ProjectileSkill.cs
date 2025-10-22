using UnityEngine;

public class ProjectileSkill : Skill
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRB;
    
    private void Awake()
    {
        spCost = 3;
        
        if (playerTransform == null)
        {
            playerTransform = GetComponentInParent<Transform>();
        }
        if (playerRB == null)
        {
            playerRB = GetComponentInParent<Rigidbody2D>();
        }
    }
    
    public override void skillActivate()
    {
        Vector2 direction;
        
        if (playerRB.linearVelocity.magnitude > 0.1f)
        {
            direction = playerRB.linearVelocity.normalized;
        }
        else
        {
            direction = Vector2.down;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        
        Instantiate(projectilePrefab, playerTransform.position, rotation);
    }
}