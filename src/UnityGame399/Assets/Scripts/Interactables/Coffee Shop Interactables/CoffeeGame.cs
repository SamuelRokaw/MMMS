using UnityEngine;

public class CoffeeGame : Interactable
{
    [SerializeField] private GameObject coffeeMinigame;
    
    public override void Interact()
    {
        if (!used)
        {
            used = true;
            Logger.Instance.Info("Coffee Grinder interact");
            CoffeeGameLoader.Instance.LoadMinigame(coffeeMinigame);
        }
    }

    public void resetUseState()
    {
        used = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
