using UnityEngine;
using System.Collections.Generic;

public class Area : MonoBehaviour
{
    [SerializeField]
    private CameraController camController;
    [SerializeField]
    private InputHandler iH;
    [SerializeField] 
    private Camera owCam;
    [SerializeField] 
    private OverworldMovement owM; 
    [SerializeField] 
    private OverworldInteraction owI; 
    
    public List<Interactable> interactables;
    public string AreaName;
    private void Awake()
    {
        iH = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();
        camController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        iH.owM  = owM;
        iH.owI = owI;
        camController.oWCam = owCam;
        camController.owCamActive();
        LoadFromPlayerPrefs();
    }
    
    public void SaveToPlayerPrefs()
    {
        AreaData data = Save();
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(AreaName, json);
        PlayerPrefs.Save();
    }

    public void LoadFromPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey(AreaName)) return;
        string json = PlayerPrefs.GetString(AreaName);
        AreaData data = JsonUtility.FromJson<AreaData>(json);
        Load(data);
    }
    public void Load(AreaData data)
    {
        int index = 0;
        foreach (char c in data.interactablsStatus)
        {
            if (c == '0')
            {
                interactables[index].used = false;
            }
            else
            {
                interactables[index].used = true;
            }
            index++;
        }
    }
    public AreaData Save()
    {
        string tempstring = "";
        foreach (Interactable i in interactables)
        {
            if (!i.used)
            {
                tempstring += '0';
            }
            else
            {
                tempstring += '1';
            }
        }
        return new AreaData
        {
            interactablsStatus = tempstring
        };
    }
}
