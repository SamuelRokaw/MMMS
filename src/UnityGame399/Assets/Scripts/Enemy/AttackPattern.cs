using UnityEngine;

public class AttackPattern : MonoBehaviour
{
    public virtual void attack()
    {
        Logger.Instance.Info("Enemy used attack pattern");
    }
}
