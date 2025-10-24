using UnityEngine;

public class EnemyDeathEffectsManager : MonoBehaviour
{
    public static EnemyDeathEffectsManager Instance { get; private set; }
    
    // death effects
    public AudioClip defaultDeathSound;
    public ParticleSystem defaultBloodSplatter;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}