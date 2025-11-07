using System;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    public float bulletRotationSpeed = 15f;
    private Rigidbody2D _rb;
    public Transform spriteTransform;
    
    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move();
        rotate();
    }

    private void move()
    {
        Vector2 direction = transform.right;
        _rb.MovePosition(_rb.position + direction * bulletSpeed * Time.fixedDeltaTime);
    }

    private void rotate()
    {
        spriteTransform.Rotate(Vector3.forward, bulletRotationSpeed * Time.fixedDeltaTime, Space.Self);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CombatPlayer"))
        {
            PlayerStatEvents.PlayerTakesDamage(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("CombatBarrier"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            WaveDefense.Takedamage(-1);
            Destroy(gameObject);
        }
    }
}
