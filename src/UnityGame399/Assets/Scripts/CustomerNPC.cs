using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class CustomerNPC : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] public SpriteRenderer CustomerSpriteRenderer;
    public Customer Customer { get; private set; }
    public Coffee Coffee { get; private set; }
    public int orderIndex;
    
    [SerializeField] public float moveSpeed = 5f;
    private bool hasCollided = false;
    private Rigidbody2D rb;
    void Start()
    {
        CustomerSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        Customer = new Customer();
        Coffee = new Coffee();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    
    void FixedUpdate()
    {
        if (!hasCollided)
        {
            rb.linearVelocity = Vector2.up * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;
    }

    public void ResumeMove()
    {
        Logger.Instance.Info($"Customer {Customer.Name} is resuming movement.");
        hasCollided = false;
    }

    public void OnDestroy()
    {
        Logger.Instance.Info("Customer NPC Destroyed");
    }

    public string Order()
    {
        Logger.Instance.Info($"Customer {Customer.Name} has ordered their coffee.");
        return $"Name: {Customer.Name}\nCaffeinated: {Coffee.IsCaffeinated}\nCreamer: {Coffee.CreamPercent * 100}% {Coffee.CreamerType}";
    }
}
