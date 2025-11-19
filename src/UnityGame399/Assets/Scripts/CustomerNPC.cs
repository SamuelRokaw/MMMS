using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[Serializable]
public class CustomerNPC : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public SpriteRenderer CustomerSpriteRenderer;
    [SerializeField] public GameObject Canvas;
    public GameObject dialoguePrefab;
    public Customer Customer { get; private set; }
    public Coffee Coffee { get; private set; }
    public int orderIndex;
    
    [SerializeField] public float moveSpeed = 5f;
    private bool hasCollided = false;
    private Rigidbody2D rb;
    public bool isMoving = false;
    
    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
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
            isMoving = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.linearVelocity = Vector2.up * moveSpeed;
        }
        else
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("OverWorldPlayer"))
        {
            hasCollided = true;
        }
    }
    
    // public override void Interact()
    // {
    //     Logger.Instance.Info($"Interact with customer {Customer.Name}.");
    //     // Instantiate dialogue prefab
    // }
    
    public void finishDialogue()
    {
        Logger.Instance.Info($"Customer {Customer.Name} has finished dialogue.");
        //TODO: Customer Manager Order
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
    public void CreateOrderDialogue()
    {
        GameObject DialogInstance = Instantiate(dialoguePrefab);
        DialogInstance.GetComponent<Dialog>().dialog[0] += $"{Customer.Name}";
        DialogInstance.GetComponent<Dialog>().dialog[2] += $"{Coffee.BeanType} {Coffee.CreamPercent * 100}% {Coffee.CreamerType} coffee";
    
        DialogInstance.transform.Find("Panel").transform.Find("Background").transform.Find("Photo").GetComponent<Image>().sprite = CustomerSpriteRenderer.sprite;
        Logger.Instance.Info($"Customer {Customer.Name} has ordered their coffee.");
    }
    
    public string GetOrderString()
    {
        return $"Name: {Customer.Name}\nBean: {Coffee.BeanType}\nCreamer: {Coffee.CreamPercent * 100}% {Coffee.CreamerType}";
    }

    public string Order()
    {
        GameObject DialogInstance = Instantiate(dialoguePrefab);
        DialogInstance.GetComponent<Dialog>().dialog[0] += $"{Customer.Name}";
        DialogInstance.GetComponent<Dialog>().dialog[2] += $"{Coffee.BeanType} {Coffee.CreamPercent * 100}% {Coffee.CreamerType} coffee";
        
        DialogInstance.transform.Find("Panel").transform.Find("Background").transform.Find("Photo").GetComponent<Image>().sprite = CustomerSpriteRenderer.sprite;
        Logger.Instance.Info($"Customer {Customer.Name} has ordered their coffee.");
        return $"Name: {Customer.Name}\nCaffeinated: {Coffee.BeanType}\nCreamer: {Coffee.CreamPercent * 100}% {Coffee.CreamerType}";
    }

    public int FinishOrder(int luck)
    {
        return (int)tip(luck);
    }

    private double tip(int luck)
    {
        int TipChance = Customer.TipChance + luck;
        int doesTip = Random.Range(0, 10);
        if (doesTip <= TipChance)
        {
            Logger.Instance.Info($"Customer {Customer.Name} tipped ${Customer.TipAmount}");
            return Customer.TipAmount;
        }
        else
        {
            Logger.Instance.Info($"Customer {Customer.Name} did not tip.");
            return 0;
        }
    }
    
    public CustomerPersonality PersonalityReturnFix()
    {
        return Customer.Personality;
    }
}
