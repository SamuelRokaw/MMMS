using UnityEngine;

[System.Serializable]
public class Trash : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] Sprite emptySprite;
    [SerializeField] Sprite fullSprite;
    private int maxTrash = 3;
    private int currentTrash = 0;
    public bool IsFull { get; private set; }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void TryTrash(int trashChance)
    {
        int tip = Random.Range(0, 10);
        if (tip <= trashChance)
        {
            Logger.Instance.Info("Customer has trash");
            AddTrash();
        }
        else
        {
            Logger.Instance.Info("Trash Not Added");
        }
    }

    public void EmptyTrash()
    {
        //Player.grabTrash();
        currentTrash = 0;
        sr.sprite = emptySprite;
        IsFull = false;
        Logger.Instance.Info("Trash Emptied");
    }
    
    private void AddTrash()
    {
        currentTrash++;
        Logger.Instance.Info($"Trash Added. Current Trash: {currentTrash}/{maxTrash}");
        if (currentTrash >= maxTrash)
        {
            IsFull = true;
            sr.sprite = fullSprite;
            Logger.Instance.Info("Trash is Full");
        }
    }
}
