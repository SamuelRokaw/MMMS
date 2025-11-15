using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class CustomerManager : Interactable
{
    [SerializeField] public GameObject customerPrefab;
    [SerializeField] public TextMeshProUGUI orderTicketText;
    [SerializeField] public TrashCan trashCan;
    [SerializeField] public Stats playerStats;
    [SerializeField] public GameObject[] dialoguePrefabs;
    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    private Queue<GameObject> customersToOrder = new Queue<GameObject>();
    private Vector3 spawnPosition = new Vector3(-6, -3, 0);
    private Quaternion spawnRotation = Quaternion.identity;

    public void SpawnCustomer()
    {
        GameObject Customer = Instantiate(customerPrefab, spawnPosition, spawnRotation);

        int dialogIndex = -1;
        Customer.GetComponent<CustomerNPC>();
        Customer c = Customer.GetComponent<CustomerNPC>().Customer;
        Logger.Instance.Info($"Tipchance: {c.TipChance}");
        Logger.Instance.Info($"Personality: {c.Personality}");
        CustomerPersonality p = Customer.GetComponent<CustomerNPC>().Customer.Personality;
        switch (Customer.GetComponent<CustomerNPC>().Customer.Personality)
        {
            case CustomerPersonality.Neutral:
                dialogIndex = 0;
                break;
            case CustomerPersonality.Mean:
                dialogIndex = 1;
                break;
            case CustomerPersonality.SammyRokaw:
                dialogIndex = 2;
                break;
            case CustomerPersonality.Nice:
                dialogIndex = 3;
                break;
            case CustomerPersonality.Timid:
                dialogIndex = 04;
                break;
            default:
                Logger.Instance.Warn("Customer Personality not recognized. Defaulting to Neutral dialogue.");
                dialogIndex = 0;
                break;
        }
        
        Customer.GetComponent<CustomerNPC>().dialoguePrefab = dialoguePrefabs[dialogIndex];
        customerQueue.Enqueue(Customer);
        Logger.Instance.Info($"Customer Spawned. Queue Length: {customerQueue.Count}");
    }
    
    public override void Interact()
    {
        Logger.Instance.Info("Interacted with Customer Manager.");
        if(customerQueue.Count > 0)
        {
            if (customerQueue.Peek().GetComponent<CustomerNPC>().isMoving == false)
            {
                Order();
            }
            else
            {
                Logger.Instance.Info("Cannot move customer to order. Current ordering customer is still moving.");
            }
        }
        else
        {
            Logger.Instance.Info("Cannot move customer to order. No customers to order.");
        }
    }

    public void Order()
    {
        GameObject orderingCustomer = customerQueue.Dequeue();
        customersToOrder.Enqueue(orderingCustomer);
        orderTicketText.text = orderingCustomer.GetComponent<CustomerNPC>().Order();
        Logger.Instance.Info($"Customer Moved to Order. Customers to Order Length: {customersToOrder.Count}");
    }

    public void Right()
    {
        if (!customersToOrder.TryDequeue(out GameObject customer))
        {
            orderTicketText.text = "No Orders";
            Logger.Instance.Info("No Customers to Move Right");
            return;
        }
        customersToOrder.Enqueue(customer);
        orderTicketText.text = customersToOrder.Peek().GetComponent<CustomerNPC>().Order();
        Logger.Instance.Info("Next Customer Displayed");
    }

    public void FinishOrder()
    {
        GameObject submittedCustomer = customersToOrder.Dequeue();
        playerStats.ChangeGold(submittedCustomer.GetComponent<CustomerNPC>().FinishOrder(playerStats.Luck));
        Logger.Instance.Info($"Trash Chance Calculation: Base {submittedCustomer.GetComponent<CustomerNPC>().Customer.TrashChance} + Luck {playerStats.Luck}");
        trashCan.TryTrash(submittedCustomer.GetComponent<CustomerNPC>().Customer.TrashChance + playerStats.Luck);
        
        foreach (GameObject customer in customersToOrder)
        {
            customer.GetComponent<CustomerNPC>().ResumeMove();
        }
        foreach (GameObject customer in customerQueue)
        {
            customer.GetComponent<CustomerNPC>().ResumeMove();
        }
        
        Destroy(submittedCustomer);
        if (customersToOrder.Count() == 0)
        {
            orderTicketText.text = "No Orders";
            Logger.Instance.Info("Finished last order. No more customers to order.");
            return;
        }
        orderTicketText.text = customersToOrder.Peek().GetComponent<CustomerNPC>().Order();
        Logger.Instance.Info("Order Finished. Next Customer Displayed");
    }
}
