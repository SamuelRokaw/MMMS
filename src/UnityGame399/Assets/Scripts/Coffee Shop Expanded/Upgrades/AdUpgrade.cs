using UnityEngine;

public class AdUpgrade : Upgrades
{
    public int amountToIncreaseBy = 5;
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
}
