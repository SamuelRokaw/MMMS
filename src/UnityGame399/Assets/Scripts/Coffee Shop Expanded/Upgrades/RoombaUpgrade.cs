using UnityEngine;

public class RoombaUpgrade : Upgrades
{
    public GameObject roomba;
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
        roomba.SetActive(true);
    } 
}
