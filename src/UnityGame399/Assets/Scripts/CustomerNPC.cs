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
    void Start()
    {
        CustomerSpriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        Customer = new Customer();
        Coffee = new Coffee();
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
