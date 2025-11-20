using UnityEngine;

public class GrinderUpgrade : Upgrades
{
    public int amountToDecreaseBy = 7;
    public CoffeeGrinderManager grinderManager;
    void Start()
    {
        if (unlocked == true)
        {
            Unlock();
        }
    }

    public override void Unlock()
    {
        base.Unlock();
        grinderManager.clicksRequired -= amountToDecreaseBy;
    }
}
