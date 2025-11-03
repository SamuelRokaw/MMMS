using UnityEngine;
using UnityEngine.UI;

public class AirView : MonoBehaviour
{
    //[SerializeField] private PlayerStatViewModel viewModel;
    [SerializeField] private Slider airSlider;

    private void OnEnable()
    {
        //PlayerStatViewModel.OnAirChanged += UpdateAir;
    }

    private void OnDisable()
    {
        //PlayerStatViewModel.OnAirChanged -= UpdateAir;
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