using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillView : MonoBehaviour
{
    [SerializeField] private PlayerStatViewModel viewModel;
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform skillContainer;
    [SerializeField] private Sprite fullSkill;
    [SerializeField] private Sprite emptySkill;

    private List<Image> skillIcons = new List<Image>();

    private void OnEnable()
    {
        viewModel.OnSkillPointsChanged += UpdateSkills;
    }

    private void OnDisable()
    {
        viewModel.OnSkillPointsChanged -= UpdateSkills;
    }

    private void UpdateSkills(int current, int max)
    {
        if (skillIcons.Count != max)
        {
            foreach (Transform child in skillContainer)
                Destroy(child.gameObject);
            skillIcons.Clear();

            for (int i = 0; i < max; i++)
            {
                var icon = Instantiate(skillPrefab, skillContainer);
                skillIcons.Add(icon.GetComponent<Image>());
            }
        }

        for (int i = 0; i < skillIcons.Count; i++)
            skillIcons[i].sprite = (i < current) ? fullSkill : emptySkill;
    }
}