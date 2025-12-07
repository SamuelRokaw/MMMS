using UnityEngine;

public class SceneInteractor : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneSwitcher.Instance.LoadMainScene();
    }
}
