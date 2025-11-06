using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class CustomerManager : MonoBehaviour
{
    [SerializeField] public GameObject customerPrefab;
    [SerializeField] public TextMeshProUGUI orderTicketText;
    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    private Queue<GameObject> customersToOrder = new Queue<GameObject>();
    private Vector3 spawnPosition = new Vector3(-6, -3, 0);
    private Quaternion spawnRotation = Quaternion.identity;

    public void SpawnCustomer()
    {
        customerQueue.Enqueue(Instantiate(customerPrefab, spawnPosition, spawnRotation));
        Logger.Instance.Info($"Customer Spawned. Queue Length: {customerQueue.Count}");
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
