using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuDefaultButton : MonoBehaviour
{
    [SerializeField] private GameObject defaultButton;

    private void OnEnable()
    {
        // Clear any existing selection
        EventSystem.current.SetSelectedGameObject(null);

        // Set your default button
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }
}
