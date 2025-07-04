using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private List<CatData> catData;
    [SerializeField] private List<float> speedLevels;
    [SerializeField] private List<float> jumpLevels;

    public void UpdateSkill(string skillName, int state)
    {
        foreach (CatData cat in catData)
        {
            if (cat.id != 4)
            { 
                float skillLevel = PlayerPrefs.GetFloat(skillName + "nameOfCat" + cat.id);          

                if (skillName == "Speed")
                {
                    if (skillLevel == 0)
                        skillLevel = cat.startSpeedSkill;

                    int index = speedLevels.IndexOf(skillLevel);

                    if (state == 1)
                        PlayerPrefs.SetFloat(skillName + "nameOfCat" + cat.id, speedLevels[index - 1]);                 
                    else
                        PlayerPrefs.SetFloat(skillName + "nameOfCat" + cat.id, speedLevels[index + 1]);                  
                }
                else
                {
                    if (skillLevel == 0)
                        skillLevel = cat.startJumpSkill;

                    int index = jumpLevels.IndexOf(skillLevel);

                    if (state == 1)
                        PlayerPrefs.SetFloat(skillName + "nameOfCat" + cat.id, jumpLevels[index - 1]);
                    else
                        PlayerPrefs.SetFloat(skillName + "nameOfCat" + cat.id, jumpLevels[index + 1]);             
                }
            }

            //Debug.Log("SkillManager /// UpdateSkill /// Cat Id = " + cat.id + " /// Skill Name = " + skillName + " /// Skill Level = " + PlayerPrefs.GetFloat(skillName + "nameOfCat" + cat.id) + " /// State = " + state);

        }
    }
}
