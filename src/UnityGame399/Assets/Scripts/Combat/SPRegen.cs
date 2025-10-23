using UnityEngine;
using System.Collections;

public class SPRegen : MonoBehaviour
{
    public Stats stats;
    private bool currentlyRegen = false;
    public void OnEnable()
    {
        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        PlayerStatEvents.DecreaseSP += regenSP;
    }

    private void Unsubscribe()
    {
        PlayerStatEvents.DecreaseSP -= regenSP;
    }

    private void regenSP(int amount) //amount won't be used
    {
        if(!currentlyRegen)
        {
            StartCoroutine(regen());
        }
    }

    IEnumerator regen()
    {
        currentlyRegen = true;
        while(stats.CurrentSP < stats.MaxSP)
        {
            yield return new WaitForSeconds(15f);
            PlayerStatEvents.IncreaseSP(1);
        }
        currentlyRegen = false;
    }
}
