using UnityEngine;

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
    private void Awake()
    {
        iH = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();
        camController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraController>();
        iH.owM  = owM;
        iH.owI = owI;
        camController.oWCam = owCam;
        camController.owCamActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
