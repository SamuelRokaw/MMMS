using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //movement
    public float speed = 1f;
    public float spdMod = 1f;
    public float rotationSpeed = 15f;
    public float rotspdMod = 1f;
    private Vector2 movement;
    private Rigidbody2D _rb;
    
    //player tracking
    private Transform _player;
    private Vector2 DirectionToPlayer;
    
    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _player = GetComponent<BasicEnemy>().player;
    }

    void Update()
    {
        DistanceToPlayer();
    }

    void FixedUpdate()
    {
        moveToPlayer();
        RotateToPlayer();
    }

    private void DistanceToPlayer()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        // Determine direction towards the player
        Vector2 direction = (_player.position - transform.position).normalized;
        movement = direction;
    }

    private void moveToPlayer()
    {
        _rb.MovePosition(_rb.position + movement * speed * spdMod * Time.fixedDeltaTime);
    }

    private void RotateToPlayer()
    {
        Vector2 dir = (Vector2)(_player.position - transform.position);
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, rotspdMod * rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(smoothedAngle);
    }
}
