using UnityEngine;

public class GrinderUpgrade : Upgrades
{
    public int amountToDecreaseBy;
    void Start()
    {
        if (unlocked == true)
        {
            Unlock();
        }
        else
        {
            this.enabled = false;
        }
    }

    public override void Unlock()
    {
        base.Unlock();
        this.enabled = true;
    }
}
