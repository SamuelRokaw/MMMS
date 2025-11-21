using System;
using UnityEngine;

public class Roomba : MonoBehaviour
{
    //movement
    public float speed = 1f;
    public float spdMod = 1f;
    public float rotationSpeed = 15f;
    public float rotspdMod = 1f;
    private Vector2 movement;
    private Rigidbody2D _rb;
    private Transform _target;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Logger.Instance.Info("Collided");
        if (other.gameObject.tag == "Trash")
        {
            other.gameObject.GetComponent<Trash>().Interact();
            _target = null;
            Logger.Instance.Info("Collided with trash");
        }
    }
    
    
    
    private void Awake()
    {
            
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if(_target == null)
        {
            FindTarget();
        }
        else
        {
            RotateToTarget(); 
            MoveToTarget();
        }
    }


    private void FindTarget()
    {
        if(GameObject.FindGameObjectWithTag("Trash") != null)
        {
            _target = GameObject.FindGameObjectWithTag("Trash").transform;
        }
    }
    private void MoveToTarget()
    {
        movement = (_target.position - transform.position).normalized;
        
        _rb.MovePosition(_rb.position + movement * speed * spdMod * Time.fixedDeltaTime);
        Logger.Instance.Info("Moved to target");
    }

    private void RotateToTarget()
    {
        Vector2 dir = (Vector2)(_target.position - transform.position);
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.LerpAngle(_rb.rotation, targetAngle, rotspdMod * rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(smoothedAngle);
        Logger.Instance.Info("Rotated to target");
    }
}
