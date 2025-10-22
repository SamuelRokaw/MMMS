using UnityEngine;

public class StartUp : MonoBehaviour
{
    public Logger logger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logger.createLogFile();
    }
    
}
