using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthView : MonoBehaviour
{
    //[SerializeField] private PlayerStatViewModel viewModel;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> hearts = new List<Image>();

    private void OnEnable()
    {
        PlayerStatViewModel.OnHeartsChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        PlayerStatViewModel.OnHeartsChanged -= UpdateHearts;
    }

    private void UpdateHearts(int current, int max)
    {
        if (hearts.Count != max)
        {
            foreach (Transform child in heartsContainer)
                Destroy(child.gameObject);
            hearts.Clear();

            for (int i = 0; i < max; i++)
            {
                var heart = Instantiate(heartPrefab, heartsContainer);
                hearts.Add(heart.GetComponent<Image>());
            }
        }

        for (int i = 0; i < hearts.Count; i++) {
            hearts[i].sprite = (i < current) ? fullHeart : emptyHeart;
        } 
    }
}