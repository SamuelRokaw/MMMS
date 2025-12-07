using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SliderNavigationFix : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Selectable nextSelectable;     // InputField or next UI element
    [SerializeField] private Selectable previousSelectable; // optional
    [SerializeField] private InputActionReference navigateAction; // reference to UI/Navigate

    private void OnEnable()
    {
        navigateAction.action.performed += OnNavigate;
    }

    private void OnDisable()
    {
        navigateAction.action.performed -= OnNavigate;
    }

    private void OnNavigate(InputAction.CallbackContext ctx)
    {
        // Only intercept if slider is selected
        if (EventSystem.current.currentSelectedGameObject != slider.gameObject)
            return;

        Vector2 input = ctx.ReadValue<Vector2>();

        // Handle horizontal input ourselves
        if (Mathf.Abs(input.x) > 0.1f)
        {
            // Stop Unity from also navigating
            EventSystem.current.SetSelectedGameObject(slider.gameObject);

            if (input.x > 0.1f)
            {
                if (slider.value < slider.maxValue)
                {
                    slider.value += 1f; // or expose stepSize
                }
                else
                {
                    nextSelectable?.Select();
                }
            }
            else if (input.x < -0.1f)
            {
                if (slider.value > slider.minValue)
                {
                    slider.value -= 1f;
                }
                else
                {
                    previousSelectable?.Select();
                }
            }
        }

        // Vertical input untouched → Unity navigation handles Up/Down
    }
}