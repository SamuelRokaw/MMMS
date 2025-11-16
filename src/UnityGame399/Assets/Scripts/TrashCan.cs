using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TrashCan : Interactable
{
    private SpriteRenderer sr;
    [SerializeField] private GameObject TrashPrefab;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullSprite;
    [SerializeField] List<Transform> trashSpawnPoints;
    [SerializeField] GameObject trashBag;
    private List<bool> trashSpawnState;
    private int maxTrashOnFloor = 3;
    private int maxTrashInCan = 5;
    private int currentTrashInCan = 0;
    private int currentTrashOnFloor = 0;
    public bool IsFull { get; private set; }
    void Start()
    {
        trashSpawnState = new List<bool>(maxTrashOnFloor);
        Logger.Instance.Info($"trashSpawnState initialized with capacity {trashSpawnState.Capacity}");
        for (int i = 0; i < maxTrashOnFloor; ++i)
        {
            Logger.Instance.Info($"Adding false to trashSpawnState at index {i}");
            trashSpawnState.Add(false);
        }
        sr = GetComponent<SpriteRenderer>();
    }

    public void TryTrash(int trashChance)
    {
        int doesSpawn = Random.Range(0, 10);
        if (doesSpawn <= trashChance)
        {
            Logger.Instance.Info("Customer has trash");
            CreateTrashPickup();
        }
        else
        {
            Logger.Instance.Info("Trash Not Added");
        }
    }
    
    private void CreateTrashPickup()
    {
        if (currentTrashOnFloor >= maxTrashOnFloor)
        {
            Logger.Instance.Info("Too Much Trash on floor. Cannot Add More Trash.");
            return;
        }

        int spawnIndex = Random.Range(0, trashSpawnPoints.Count);
        Transform spawnPoint = trashSpawnPoints[spawnIndex];

        if (trashSpawnState[spawnIndex] == true)
        {
            for (int i = 0; i < trashSpawnPoints.Count; ++i)
            {
                if (trashSpawnState[i] == false)
                {
                    spawnPoint = trashSpawnPoints[i];
                    spawnIndex = i;
                    break;
                }
            }
        }

        currentTrashOnFloor++;
        trashSpawnState[spawnIndex] = true;
        GameObject tempTrash = Instantiate(TrashPrefab, spawnPoint.position, Quaternion.identity);
        tempTrash.GetComponent<Trash>().trashCan = this;

    }

    public override void Interact()
    {
        if(currentTrashInCan > 0)
        {
            trashBag.SetActive(true);
            StateManager.Instance.SwitchToTakingOutTrash();
            for (int i = 0; i < trashSpawnPoints.Count; ++i)
            {
                trashSpawnState[i] = false;
            }

            currentTrashInCan = 0;
            sr.sprite = emptySprite;
            IsFull = false;
            Logger.Instance.Info("Trash Emptied");
        }
    }
    
    public void AddTrashToCan()
    {
        currentTrashOnFloor--;
        currentTrashInCan++;
        Logger.Instance.Info($"Trash Added. Current Trash: {currentTrashInCan}/{maxTrashInCan}");
        if (currentTrashInCan == maxTrashInCan)
        {
            IsFull = true;
            sr.sprite = fullSprite;
            Logger.Instance.Info("Trash is Full");
        }
    }
}