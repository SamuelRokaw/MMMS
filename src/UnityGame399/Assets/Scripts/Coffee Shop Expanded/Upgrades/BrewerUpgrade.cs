using UnityEngine;

public class BrewerUpgrade : Upgrades
{
    public GameObject secondBrewer;
    void Start()
    {
        if (unlocked == true)
        {
            Unlock();
        }
        else
        {
            secondBrewer.SetActive(false);
        }
    }
    public override void Unlock()
    {
        base.Unlock();
        secondBrewer.SetActive(true);
    }
}
