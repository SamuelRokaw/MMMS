using UnityEngine;

public class ToppingUpgrade : Upgrades
{
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
