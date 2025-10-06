using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Scenes : MonoBehaviour
{
    [SerializeField]
    private string startScene = "Test_Tom_Start";
    [SerializeField]
    private string gameScene = "SampleScene";
    
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    
    
}
