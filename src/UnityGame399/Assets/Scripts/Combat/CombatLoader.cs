using System.Collections;
using UnityEngine;

public class CombatLoader : MonoBehaviour
{
    public static CombatLoader Instance;
    private void Awake() // singleton???
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    public void LoadCombat(GameObject encounterPrefab)
    {
        Debug.Log("Starting combat + combat transition");
        
        if (encounterPrefab != null)
        {
            // fade to black 
            StartCoroutine(LoadCombatWithFade(encounterPrefab));
        }
        else
        {
            Debug.LogWarning("Encounter prefab is null");
        }
    }


    private IEnumerator LoadCombatWithFade(GameObject encounterPrefab)
    {
        yield return StartCoroutine(FadeController.Instance.FadeToBlack());

        GameObject encounter = Instantiate(encounterPrefab);

        encounter.SetActive(false);
        
        encounter.SetActive(true);

        yield return StartCoroutine(FadeController.Instance.FadeFromBlack());
        
        yield return new WaitForSeconds(1.2f);
    }
    void Update()
    {
        
    }
}
