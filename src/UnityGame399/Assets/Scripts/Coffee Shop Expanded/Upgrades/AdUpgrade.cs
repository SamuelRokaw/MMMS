using UnityEngine;

public class AdUpgrade : Upgrades
{
    public int amountToIncreaseBy;
    public Cycle cycle;
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

    }

    public void increaseCustomerSpawnChance()
    {
        
    }
}
