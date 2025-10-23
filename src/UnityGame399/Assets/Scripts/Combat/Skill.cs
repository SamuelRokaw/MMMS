using UnityEngine;

public class Skill : MonoBehaviour
{
    public int spCost = 1;
    
    public virtual void skillActivate(int currentSP)
    {
        Debug.Log("skill used");
        
    }
}
