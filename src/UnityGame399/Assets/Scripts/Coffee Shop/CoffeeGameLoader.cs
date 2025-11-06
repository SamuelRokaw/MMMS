using System.Collections;
using UnityEngine;

public class CoffeeGameLoader : MonoBehaviour
{
    public static CoffeeGameLoader Instance;
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
    } 
    void Start()
    {
        
    }

    // Update is called once per frame

    public void LoadMinigame(GameObject minigame)
    {
        Debug.Log("Starting minigame + minigame transition");
        
        if (minigame != null)
        {
            // fade to black 
            StartCoroutine(LoadMinigameWithFade(minigame));
        }
        else
        {
            Debug.LogWarning("minigame prefab is null");
        }
    }


    private IEnumerator LoadMinigameWithFade(GameObject minigame)
    {
        bTO.alpha = 1;
        bTO.interactable = true;
        bTO.blocksRaycasts = true;
        
        yield return StartCoroutine(FadeController.Instance.FadeToBlack());

        minigame.SetActive(false);
        
        minigame.SetActive(true);

        yield return StartCoroutine(FadeController.Instance.FadeFromBlack());
        
        yield return new WaitForSeconds(0.5f);
        
        bTO.alpha = 0;
        bTO.interactable = false;
        bTO.blocksRaycasts = false;
    }
    
    public void UnloadMinigame(GameObject minigame)
    {
        Debug.Log("ending minigame + minigame transition");
        
        if (minigame != null)
        {
            // fade to black 
            StartCoroutine(UnloadMinigameWithFade(minigame));
        }
        else
        {
            Debug.LogWarning("Encounter prefab is null");
        }
    }


    private IEnumerator UnloadMinigameWithFade(GameObject minigame)
    {
        bTO.alpha = 1;
        bTO.interactable = true;
        bTO.blocksRaycasts = true;
        
        yield return StartCoroutine(FadeController.Instance.FadeToBlack());
        
        minigame.SetActive(true);
        
        minigame.SetActive(false);

        yield return StartCoroutine(FadeController.Instance.FadeFromBlack());
        
        yield return new WaitForSeconds(0.5f);
        
        bTO.alpha = 0;
        bTO.interactable = false;
        bTO.blocksRaycasts = false;
    }
    
    
    void Update()
    {
        
    }
}