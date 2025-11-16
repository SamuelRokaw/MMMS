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
    
    //wave defense tracking
    [SerializeField] private bool isWaveDefense = false;
    private Transform _target;
    private Vector2 DirectionToTarget;
    private bool trackingPlayer = true;
    
    private void Awake()
    {
        if (isWaveDefense)
        {
            _target = GameObject.FindGameObjectWithTag("Target").transform;
        }
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _player = GetComponent<BasicEnemy>().player;
    }

    void Update()
    {
        DistanceToTarget();
    }

    void FixedUpdate()
    {
        moveToTarget();
        RotateToTarget();
    }

    private void DistanceToTarget()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
        if (isWaveDefense)
        {
            if (distanceToPlayer <= 6)
            {
                // Determine direction towards the player
                Vector2 direction = (_player.position - transform.position).normalized;
                movement = direction;
                trackingPlayer = true;
            }
            else
            {
                // Determine direction towards the wave defense structure
                Vector2 direction = (_target.position - transform.position).normalized;
                movement = direction;
                trackingPlayer = false;
            }
        }
        else
        {
            // Determine direction towards the player
            Vector2 direction = (_player.position - transform.position).normalized;
            movement = direction;
        }
    }

    private void moveToTarget()
    {
        _rb.MovePosition(_rb.position + movement * speed * spdMod * Time.fixedDeltaTime);
    }

    private void RotateToTarget()
    {
        if(trackingPlayer)
        {
            Vector2 dir = (Vector2)(_player.position - transform.position);
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, rotspdMod * rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(smoothedAngle);
        }
        else
        {
            Vector2 dir = (Vector2)(_target.position - transform.position);
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, rotspdMod * rotationSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(smoothedAngle);
        }
    }
}
