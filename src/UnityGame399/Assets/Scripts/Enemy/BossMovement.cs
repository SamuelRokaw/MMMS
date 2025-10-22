using UnityEngine;

public class BossEnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float spdMod = 1f;
    public float rotationSpeed = 25f;
    public float rotspdMod = 1f;
    
    private Rigidbody2D _rb;
    private Transform targetNode;
    private string lastWallTag = "";
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (targetNode != null)
        {
            MoveToTarget();
            RotateToTarget();
        }
    }
    
    public void SetTargetNode(Transform node, string wallTag)
    {
        targetNode = node;
        lastWallTag = wallTag;
    }
    
    public string GetLastWallTag()
    {
        return lastWallTag;
    }
    
    public bool HasReachedTarget()
    {
        if (targetNode == null)
        {
            return true;
        }
        
        float distance = Vector2.Distance(transform.position, targetNode.position);
        return distance < 0.5f;
    }
    
    private void MoveToTarget()
    {
        Vector2 direction = ((Vector2)targetNode.position - _rb.position).normalized;
        _rb.MovePosition(_rb.position + direction * speed * spdMod * Time.fixedDeltaTime);
    }
    
    private void RotateToTarget()
    {
        Vector2 dir = (Vector2)(targetNode.position - transform.position);
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, rotspdMod * rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(smoothedAngle);
    }
}