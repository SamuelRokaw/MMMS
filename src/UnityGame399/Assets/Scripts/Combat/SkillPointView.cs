using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillView : MonoBehaviour
{
    //[SerializeField] private PlayerStatViewModel viewModel;
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private List<Transform> skillContainers;
    [SerializeField] private Sprite fullSkillTier1; // 1–5
    [SerializeField] private Sprite fullSkillTier2; // 6–10
    [SerializeField] private Sprite fullSkillTier3; // 11–15
    [SerializeField] private Sprite fullSkillTier4; // 16–20
    [SerializeField] private Sprite emptySkill; //empty skill sprite should only show up in row 1-5

    private List<Image> skillIcons = new List<Image>();

    private void OnEnable()
    {
        PlayerStatViewModel.OnSkillPointsChanged += UpdateSkills;
    }

    private void OnDisable()
    {
        PlayerStatViewModel.OnSkillPointsChanged -= UpdateSkills;
    }
    private void UpdateSkills(int current, int max)
    {
        // Clear all containers
        foreach (Transform container in skillContainers)
        {
            foreach (Transform child in container)
                Destroy(child.gameObject);
        }

        skillIcons.Clear();

        for (int i = 0; i < max; i++)
        {
            int containerIndex = i / 5;
            if (containerIndex >= skillContainers.Count)
            {
                Logger.Instance.Warn($"Not enough skill containers for skill index {i}");
                break;
            }

            Transform targetContainer = skillContainers[containerIndex];
            var icon = Instantiate(skillPrefab, targetContainer);
            var image = icon.GetComponent<Image>();
            skillIcons.Add(image);
        }

        for (int i = 0; i < skillIcons.Count; i++)
        {
            if (i < 5)
            {
                // Tier 1: toggle between full and empty
                skillIcons[i].sprite = (i < current) ? fullSkillTier1 : emptySkill;
                skillIcons[i].gameObject.SetActive(true);
            }
            else
            {
                // Tier 2–4: show only if earned
                skillIcons[i].gameObject.SetActive(i < current);
                if (i < current)
                    skillIcons[i].sprite = GetFullSpriteForIndex(i);
            }
        }
    }

    private Sprite GetFullSpriteForIndex(int index)
    {
        if (index < 5) return fullSkillTier1;
        if (index < 10) return fullSkillTier2;
        if (index < 15) return fullSkillTier3;
        return fullSkillTier4;
    }
}