using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public bool unlocked = false;

    public virtual void Unlock()
    {
        Logger.Instance.Info($"{this.name} has been unlocked");
        unlocked = true;
    }
}
