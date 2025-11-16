using System;
using UnityEngine;

public class rooftoggler : MonoBehaviour
{
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject alleyCam;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OverWorldPlayer"))
        {
            roof.SetActive(true);
            alleyCam.SetActive(true);
            mainCam.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OverWorldPlayer"))
        {
            roof.SetActive(false);
            alleyCam.SetActive(false);
            mainCam.SetActive(true);
        }
    }
}
