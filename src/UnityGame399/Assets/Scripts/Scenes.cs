using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Scenes : MonoBehaviour
{
    [SerializeField]
    private string startScene = "StartScreen";
    [SerializeField]
    private string gameScene = "PlayTest";
    
    public void LoadGame()
    {
        LoadScene(gameScene);
    }
    
    public void QuitToStart()
    {
        LoadScene(startScene);
    }
    
    public void QuitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    private void LoadScene(string sceneName)
    {
        Logger.Instance.Info("Loading World Scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
