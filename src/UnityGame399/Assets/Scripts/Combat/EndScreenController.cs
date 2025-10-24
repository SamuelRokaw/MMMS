using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public static EndScreenController Instance { get; private set; }
    
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private Text endText;
    [SerializeField] private CanvasGroup bTO;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        endScreenPanel.SetActive(false);
    }
    
    public void ShowWin()
    {
        Logger.Instance.Info("Player Wins combat");
        endText.text = "VICTORY!";
        endText.color = Color.green;
        endScreenPanel.SetActive(true);
    }
    
    public void ShowLose()
    {
        Logger.Instance.Info("Player Loses combat");
        endText.text = "DEFEAT";
        endText.color = Color.red;
        endScreenPanel.SetActive(true);
    }
    
    public void Hide()
    {
        endScreenPanel.SetActive(false);
        bTO.alpha = 0;
        bTO.interactable = false;
        bTO.blocksRaycasts = false;
    }
}