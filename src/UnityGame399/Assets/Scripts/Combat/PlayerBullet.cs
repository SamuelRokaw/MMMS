using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    private Rigidbody2D _rb;
    public int aP= 1;
    
    private void Awake()
    {
        aP = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>().AttackPower;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        Vector2 direction = transform.right;
        _rb.MovePosition(_rb.position + direction * bulletSpeed * Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CombatBarrier"))
        {
            Destroy(gameObject);
        }
    }
}