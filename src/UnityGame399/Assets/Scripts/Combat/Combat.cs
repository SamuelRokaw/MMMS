using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using PlayerStuff;

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

    [SerializeField] private GameObject csUI;

    [SerializeField] 
    private int xpCollected = 0;

    [SerializeField] 
    private int numEnemies = 0;
    [SerializeField] 
    private int numEnemiesKilled = 0;
    [SerializeField]
    private List<GameObject> enemies;

    [SerializeField] private int resourceType = 0; //0-bean, 1 - milk, 2-caramel
    [SerializeField] private int resource1 = 0;
    [SerializeField] private int resource2 = 0;
    
    //enemy spawning
    [SerializeField] private List<int> waves;
    [SerializeField] private List<GameObject> enemyVariants;
    [SerializeField] private List<Transform> enemiesSpawnPoints;
    [SerializeField] private int currentWave = 0;
    
    private void Awake()
    {
        stats = GameObject.FindGameObjectWithTag("GameController").GetComponent<Stats>();
        csUI = GameObject.FindGameObjectWithTag("CSUI");
        iH = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();
        camController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        iH.cC = cC;
        camController.cCam = comCam;
        if (csUI != null)
        {
            csUI.SetActive(false);
        }
        enterCombat();
    }
    
    void Start()
    {
        spawnWave(waves[0]);
        numEnemies = waves[0];
        currentWave = 0;
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
        WaveDefense.Destroyed += lose;
    }

    private void Unsubscribe()
    {
        PlayerStatEvents.Die -= lose;
        BasicEnemy.benemyDied -= gainXP;
        WaveDefense.Destroyed -= lose;
    }


    private void enterCombat()
    {
        StateManager.Instance.SwitchToCombat();
        stats.Reset();
        camController.cCamActive();
        Reset();
    }

    private void exitCombat()
    {
        StateManager.Instance.SwitchToCoffeeShop();
        camController.owCamActive();
        clearCombat();
        StopAllCoroutines();
        Unsubscribe();
        stats.Reset();
        if (csUI != null)
        {
            csUI.SetActive(true);
        }
        if (StateManager.Instance.currentShopState == ShopStates.OverTime)
        {
            StateManager.Instance.SwitchShopToTransition();
        }
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
        if (resourceType == 0)
        {
            stats.ChangeCafBean(resource1);
            stats.ChangeDecafBean(resource2);
        }
        else if (resourceType == 1)
        {
            stats.ChangeMilkCreamer(waves.Count * 10);
        }
        else if (resourceType == 2)
        {
            stats.ChangeCarCreamer(waves.Count * 10);
        }
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

    private void gainXP()
    {
        numEnemiesKilled++;
        xpCollected++;
        DropResources();
        if (numEnemiesKilled == numEnemies)
        {
            nextWave();
        }
    }

    private void Reset()
    {
        xpCollected = 0;
        numEnemiesKilled = 0;
    }

    private void DropResources()
    {
        if (resourceType == 0)//beandrop
        {
            int amountToDrop = Random.Range(1, 51);
            amountToDrop += stats.Luck;
            if (amountToDrop < 11)
            {
                resource1 += 1;
                resource2 += 1;
            }
            else if (amountToDrop > 10 && amountToDrop < 21)
            {
                resource1 += 2;
                resource2 += 2;
            }
            else if (amountToDrop > 20 && amountToDrop < 31)
            {
                resource1 += 3;
                resource2 += 3;
            }
            else if (amountToDrop > 30 && amountToDrop < 41)
            {
                resource1 += 4;
                resource2 += 4;
            }
            else if (amountToDrop > 40)
            {
                resource1 += 5;
                resource2 += 5;
            }

        }
    }

    private void nextWave()
    {
        currentWave++;
        if (currentWave >= waves.Count)
        {
            win();
        }
        else
        {
            spawnWave(waves[currentWave]);
            numEnemies = waves[currentWave];
            numEnemiesKilled = 0;
        }
    }
    private void spawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int x = Random.Range(0, 3);
            int y = Random.Range(0, enemiesSpawnPoints.Count);
            Instantiate(enemyVariants[x], enemiesSpawnPoints[y]);
        }
    }
}
