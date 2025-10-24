using UnityEngine;
using System.Collections.Generic;

public class ControlTree : Interactable
{
    public List<Tree> trees;
    public bool allTreesFound = false;
    public GameObject Dialog;
    public GameObject Dialog2;
    public GameObject attackUpgrade;
    public string treename;


    private void Awake()
    {
        if (used)
        {
            attackUpgrade.SetActive(true);
        }
    }
    public override void Interact()
    {
        Logger.Instance.Info($"{treename} interacted");
        checkTrees();
        if (allTreesFound)
        {
            if (!used)
            {
                used = true;
                Dialog2.SetActive(true);
                attackUpgrade.SetActive(true);
            }
        }
        else
        {
            Dialog.SetActive(true);
        }
        
    }

    private void checkTrees()
    {
        allTreesFound = true;
        foreach (Tree tree in trees)
        {
            if (!tree.used)
            {
                allTreesFound = false;
            }
        }
    }
}
