using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Scenes : MonoBehaviour
{
    public void LoadGame()
    {
        StateManager.Instance.SwitchToCoffeeShop();
        SceneSwitcher.Instance.LoadOtherScene(0);
    }
    
    public void QuitToStart()
    {
        StateManager.Instance.SwitchToMain();
        SceneSwitcher.Instance.LoadMainScene();
    }
    
    public void QuitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
