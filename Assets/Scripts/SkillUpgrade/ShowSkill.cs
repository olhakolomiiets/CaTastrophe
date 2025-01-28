using UnityEngine;
using System.Collections.Generic;

public class ShowSkill : MonoBehaviour
{
    [Header("Skill Settings")]
    [SerializeField] private string skillName;
    [SerializeField] private CatData catData; // Используем CatData вместо ручного catIndex

    [Header("Skill Indicators")]
    [SerializeField] private List<float> skillLevels;
    [SerializeField] private List<GameObject> skillIndicators;

    private float skillLevel;
    private int catIndex;
    private float skillLevelOnStart;

    private void Awake()
    {
        if (catData == null)
        {
            Debug.LogError("CatData не назначен в " + gameObject.name);
            return;
        }

        catIndex = catData.id; // Берем ID кота

        // Выбираем начальный уровень навыка
        if (skillName == "Jump")
            skillLevelOnStart = catData.startJumpSkill;
        else if (skillName == "Speed")
            skillLevelOnStart = catData.startSpeedSkill;
        else
            Debug.LogWarning($"Неизвестный навык: {skillName}. Используется значение по умолчанию.");

    }

    private void OnEnable()
    {
        UpdateSkill();
    }

    public void SetSkillLevel(float level)
    {
        if (skillLevels == null || skillIndicators == null || skillIndicators.Count == 0)
        {
            Debug.LogWarning("Skill indicators или skillLevels не установлены!");
            return;
        }

        int index = skillLevels.IndexOf(level);

        if (index == -1)
        {
            Debug.LogWarning($"Skill level {level} не найден в списке.");
            return;
        }

        for (int i = 0; i < skillIndicators.Count; i++)
        {
            skillIndicators[i].SetActive(i <= index);
        }
    }

    private void UpdateSkill()
    {

        if (PlayerPrefs.HasKey(skillName + "nameOfCat" + catIndex))
        {
            Debug.Log("The key " + skillName + "nameOfCat" + catIndex + " exists");

            skillLevel = PlayerPrefs.GetFloat(skillName + "nameOfCat" + catIndex);
        }
        else
        {
            Debug.Log("The key " + skillName + "nameOfCat" + catIndex + " does not exist");
            skillLevel = skillLevelOnStart;
        }

        SetSkillLevel(skillLevel);
    }

}
