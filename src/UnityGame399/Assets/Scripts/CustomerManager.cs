using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using PlayerStuff;
using TMPro;

[Serializable]
public class CustomerManager : Interactable
{
    [SerializeField] public GameObject customerPrefab;
    [SerializeField] public TextMeshProUGUI orderTicketText;
    [SerializeField] public TrashCan trashCan;
    [SerializeField] public Stats playerStats;
    [SerializeField] public GameObject[] dialoguePrefabs;
    
    [SerializeField] public Transform spawnPoint;

    private List<GameObject> waitingCustomers = new List<GameObject>();
    private List<GameObject> customersWithOrders = new List<GameObject>();
    private int currentOrderIndex = 0; 

    public void SpawnCustomer()
    {
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : new Vector3(-6, -5, 0);
        Quaternion spawnRot = spawnPoint != null ? spawnPoint.rotation : Quaternion.identity;
        
        GameObject Customer = Instantiate(customerPrefab, spawnPos, spawnRot);
        
        int dialogIndex = 0;
        Customer.GetComponent<CustomerNPC>();
        CustomerPersonality p = Customer.GetComponent<CustomerNPC>().Customer.Personality;
        
        switch (p)
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
                dialogIndex = 4;
                break;
            default:
                Logger.Instance.Warn("Customer Personality not recognized. Defaulting to Neutral dialogue.");
                dialogIndex = 0;
                break;
        }
        
        Customer.GetComponent<CustomerNPC>().dialoguePrefab = dialoguePrefabs[dialogIndex];
        waitingCustomers.Add(Customer);
        Logger.Instance.Info($"Customer Spawned. Waiting customers: {waitingCustomers.Count}");
    }
    
    public override void Interact()
    {
        Logger.Instance.Info("Interacted with Customer Manager.");
        if (waitingCustomers.Count > 0)
        {
            if (waitingCustomers[0].GetComponent<CustomerNPC>().isMoving == false)
            {
                ProcessNextOrder();
            }
            else
            {
                Logger.Instance.Info("Cannot move customer to order. Current customer is still moving.");
            }
        }
        else
        {
            Logger.Instance.Info("Cannot move customer to order. No customers waiting.");
        }
    }

    public void ProcessNextOrder()
    {
        if (waitingCustomers.Count == 0)
        {
            Logger.Instance.Info("No customers waiting to order.");
            return;
        }

        GameObject orderingCustomer = waitingCustomers[0];
        waitingCustomers.RemoveAt(0);
        customersWithOrders.Add(orderingCustomer);
        
        CustomerNPC npc = orderingCustomer.GetComponent<CustomerNPC>();
        npc.CreateOrderDialogue();
    
        currentOrderIndex = customersWithOrders.Count - 1;
        UpdateOrderDisplay();
    }

    public void Right()
    {
        if (customersWithOrders.Count == 0)
        {
            Logger.Instance.Info("No orders to view.");
            return;
        }

        currentOrderIndex = (currentOrderIndex + 1) % customersWithOrders.Count;
        UpdateOrderDisplay();
        Logger.Instance.Info($"Viewing order {currentOrderIndex + 1}/{customersWithOrders.Count}");
    }

    public void Left()
    {
        if (customersWithOrders.Count == 0)
        {
            Logger.Instance.Info("No orders to view.");
            return;
        }

        currentOrderIndex--;
        if (currentOrderIndex < 0)
        {
            currentOrderIndex = customersWithOrders.Count - 1;
        }
        
        UpdateOrderDisplay();
        Logger.Instance.Info($"Viewing order {currentOrderIndex + 1}/{customersWithOrders.Count}");
    }

    private void UpdateOrderDisplay()
    {
        if (customersWithOrders.Count == 0)
        {
            orderTicketText.text = "No Orders";
            return;
        }

        GameObject currentCustomer = customersWithOrders[currentOrderIndex];
        CustomerNPC npc = currentCustomer.GetComponent<CustomerNPC>();
    
        if (npc != null)
        {
            orderTicketText.text = $"Order {currentOrderIndex + 1}/{customersWithOrders.Count}\n{npc.GetOrderString()}";
        }
    }

    public void SubmitCoffee(Coffee submittedCoffee)
    {
        if (customersWithOrders.Count == 0)
        {
            Logger.Instance.Info("No orders to fulfill!");
            return;
        }

        GameObject currentCustomer = customersWithOrders[currentOrderIndex];
        CustomerNPC npc = currentCustomer.GetComponent<CustomerNPC>();
    
        if (npc == null || npc.Coffee == null)
        {
            Logger.Instance.Info("Customer has no order!");
            return;
        }
    
        int accuracy = CompareCoffees(npc.Coffee, submittedCoffee);
        Logger.Instance.Info($"Coffee submitted! Accuracy: {accuracy}%");
    
        int tipAmount = npc.FinishOrder(playerStats.Luck);
        playerStats.ChangeGold(tipAmount);
    
        trashCan.TryTrash(npc.Customer.TrashChance + playerStats.Luck);
        
        customersWithOrders.RemoveAt(currentOrderIndex);
        Destroy(currentCustomer);
    
        if (currentOrderIndex >= customersWithOrders.Count && customersWithOrders.Count > 0)
        {
            currentOrderIndex = customersWithOrders.Count - 1;
        }
        else if (customersWithOrders.Count == 0)
        {
            currentOrderIndex = 0;
        }
    
        foreach (GameObject customer in customersWithOrders)
        {
            customer.GetComponent<CustomerNPC>()?.ResumeMove();
        }
        foreach (GameObject customer in waitingCustomers)
        {
            customer.GetComponent<CustomerNPC>()?.ResumeMove();
        }

        UpdateOrderDisplay();
    }

    private int CompareCoffees(Coffee requested, Coffee submitted)
    {
        int accuracy = 0;
        int totalChecks = 3;
        
        if (requested.BeanType == submitted.BeanType)
        {
            accuracy++;
        }
        
        if (requested.CreamerType == submitted.CreamerType)
        {
            accuracy++;
        }
        
        double difference = Math.Abs(requested.CreamPercent - submitted.CreamPercent);
        if (difference <= 0.1)
        {
            accuracy++;
        }

        return (accuracy * 100) / totalChecks;
    }

    public void FinishOrder()
    {
        if (customersWithOrders.Count == 0)
        {
            Logger.Instance.Info("No orders to finish!");
            return;
        }

        GameObject submittedCustomer = customersWithOrders[currentOrderIndex];
        playerStats.ChangeGold(submittedCustomer.GetComponent<CustomerNPC>().FinishOrder(playerStats.Luck));
        trashCan.TryTrash(submittedCustomer.GetComponent<CustomerNPC>().Customer.TrashChance + playerStats.Luck);
        
        customersWithOrders.RemoveAt(currentOrderIndex);
        
        foreach (GameObject customer in customersWithOrders)
        {
            customer.GetComponent<CustomerNPC>().ResumeMove();
        }
        foreach (GameObject customer in waitingCustomers)
        {
            customer.GetComponent<CustomerNPC>().ResumeMove();
        }
        
        Destroy(submittedCustomer);
        
        if (currentOrderIndex >= customersWithOrders.Count && customersWithOrders.Count > 0)
        {
            currentOrderIndex = customersWithOrders.Count - 1;
        }
        else if (customersWithOrders.Count == 0)
        {
            currentOrderIndex = 0;
        }
        
        UpdateOrderDisplay();
        Logger.Instance.Info("Order Finished. Next Customer Displayed");
        if (StateManager.Instance.currentShopState == ShopStates.OverTime)
        {
            if(waitingCustomers.Count + customersWithOrders.Count == 0)
            {
                StateManager.Instance.SwitchShopToTransition();
            }
        }
    }

    public int GetWaitingCustomersCount() => waitingCustomers.Count;
    public int GetCustomersWithOrders() => customersWithOrders.Count;
    public int GetOrdersCount() => customersWithOrders.Count;
}