using UnityEngine;

public class CoffeeShopManager : MonoBehaviour
{
    public static CoffeeShopManager Instance;
    
    public int decafCoffeeGrounds = 0;
    public int caffeinatedCoffeeGrounds = 0;

    public int decafBrews = 0;

    public int caffeinatedBrews = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteGrinding(BeanType type)
    {
        if (type == BeanType.Caffeinated)
        {
            caffeinatedCoffeeGrounds++;
        }
        else if (type == BeanType.Decaf)
        {
            decafCoffeeGrounds++;
        }
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
