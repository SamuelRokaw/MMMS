using UnityEngine;
using UnityEngine.UI;

public class AirView : MonoBehaviour
{
    [SerializeField] private PlayerStatViewModel viewModel;
    [SerializeField] private Slider airSlider;

    private void OnEnable()
    {
        viewModel.OnAirChanged += UpdateAir;
    }

    private void OnDisable()
    {
        viewModel.OnAirChanged -= UpdateAir;
    }

    private void Start()
    {
        airSlider.minValue = 0;
    }

    private void UpdateAir(float current, float max)
    {
        airSlider.value = current / max;
    }
}