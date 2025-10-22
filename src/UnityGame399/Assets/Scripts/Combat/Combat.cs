using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour
{
    [SerializeField]
    private CameraController camController;
    [SerializeField]
    private InputHandler iH;
    [SerializeField]
    private CombatControl cC;
    [SerializeField] 
    private Camera comCam;
    [SerializeField] 
    private Stats stats;

    [SerializeField] 
    private int xpCollected = 0;

    [SerializeField] 
    private int numFish = 0;
    [SerializeField] 
    private int numFishKilled = 0;
    [SerializeField]
    private List<GameObject> fish;
    
    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
        iH = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();
        camController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        iH.cC = cC;
        camController.cCam = comCam;
        enterCombat();
    }
    
    void Start()
    {
        numFish = fish.Count;
        StartCoroutine(DepleteAir());
    }
    
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
        PlayerStatEvents.Die += lose;
        BasicEnemy.benemyDied += gainXP;
    }

    private void Unsubscribe()
    {
        PlayerStatEvents.Die -= lose;
        BasicEnemy.benemyDied -= gainXP;
    }


    private void enterCombat()
    {
        iH.inCombat = true;
        stats.Reset();
        camController.cCamActive();
        Reset();
    }

    private void exitCombat()
    {
        iH.inCombat = false;
        camController.owCamActive();
        clearCombat();
        StopAllCoroutines();
        Unsubscribe();
        stats.Reset();
        Destroy(this.gameObject);
        
    }

    private IEnumerator ExitCombatWithFade(bool playerWon)
    {
        Time.timeScale = 0f;

        yield return StartCoroutine(FadeController.Instance.FadeToBlack());

        if (playerWon)
        {
            EndScreenController.Instance.ShowWin();
        }
        else
        {
            EndScreenController.Instance.ShowLose();
        }
        yield return new WaitForSecondsRealtime(2f);

        EndScreenController.Instance.Hide();
        
        Time.timeScale = 1f;
        
        FadeController.Instance.hideBlackScreen();
        
        exitCombat();
        
    }

    private void lose()
    {
        StartCoroutine(ExitCombatWithFade(false));
    }
    private void win()
    {
        stats.GainExperience(xpCollected);
        StartCoroutine(ExitCombatWithFade(true));
    }

    private void clearCombat()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
    
    IEnumerator DepleteAir()
    {
        while(true)
        {
            PlayerStatEvents.DecreaseOxygen.Invoke(1);
            yield return new WaitForSeconds(1);
        }
    }

    private void gainXP()
    {
        numFishKilled++;
        xpCollected++;
        if (numFishKilled == numFish)
        {
            win();
        }
    }

    private void Reset()
    {
        xpCollected = 0;
        numFishKilled = 0;
    }
}
