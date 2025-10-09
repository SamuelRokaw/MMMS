using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera oWCam;
    public Camera cCam;

    public void cCamActive()
    {
        oWCam.gameObject.SetActive(false);
        if(cCam != null)
        {
            cCam.gameObject.SetActive(true);
        }
    }
    public void owCamActive()
    {
        oWCam.gameObject.SetActive(true);
        if(cCam != null)
        {
            cCam.gameObject.SetActive(false);
        }
    }
}
