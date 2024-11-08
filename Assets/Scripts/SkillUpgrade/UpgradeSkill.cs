using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSkill : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] private float skillLevelOnStart;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject skillUI;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private GameObject noMoneyTag;
    [SerializeField] private buyScript catInfo;
    [SerializeField] private GameObject upgradeButton; 

    [SerializeField] private List<float> skillLevels;
    [SerializeField] private List<float> levelsPrices; 
    [SerializeField] private List<GameObject> skillIndicators;

    private float skillLevel;
    private int TotalScore;

    private void OnEnable() 
    {
        UpdateSkill();
    }

    public void SetSkillLevel(float level)
    {
        int index = skillLevels.IndexOf(level);

        if (index == skillLevels.Count - 1)
        {
            upgradeButton.SetActive(false);
        }

        if (index == -1)
        {
            Debug.LogWarning("Skill level not found in the list.");
            return;
        }

        for (int i = 0; i < skillIndicators.Count; i++)
        {
            if (i <= index)
            {
                skillIndicators[i].SetActive(true);
            }
            else
            {
                skillIndicators[i].SetActive(false);
            }
        }
    }

    public void OpenSkillUpgadeUI()
    {
        int index = skillLevels.IndexOf(skillLevel);
        priceText.text = levelsPrices[index].ToString();
        skillUI.SetActive(true);
        buttonBuy.onClick.AddListener(PurchaseSkillUpgrade);

    }

    public void PurchaseSkillUpgrade()
    {
        int index = skillLevels.IndexOf(skillLevel);
        TotalScore = PlayerPrefs.GetInt("TotalScore");

        if (TotalScore >= levelsPrices[index])
        {
            PlayerPrefs.SetFloat(skillName + "nameOfCat" + catInfo.playerIndex, skillLevels[index + 1]);
            buttonBuy.onClick.RemoveListener(PurchaseSkillUpgrade);

            //FirebaseAnalytics.LogEvent(name: "buy_cheat_power_" + ppNameCheatPower);

            TotalScore = (int)(TotalScore - levelsPrices[index]);
            SoundManager.snd.PlaybuySounds();

            skillUI.SetActive(false);
        }
        else 
        {
            StartCoroutine(NoMoney());
        }

        UpdateSkill();
        
    }

    IEnumerator NoMoney()
    {
        noMoneyTag.SetActive(true);
        yield return new WaitForSeconds(3f);
        noMoneyTag.SetActive(false);
    }

    private void UpdateSkill()
    {

        if (PlayerPrefs.HasKey(skillName + "nameOfCat" + catInfo.playerIndex))
        {
            Debug.Log("The key " + skillName + "nameOfCat" + catInfo.playerIndex + " exists");

            skillLevel = PlayerPrefs.GetFloat(skillName + "nameOfCat" + catInfo.playerIndex);
        }
        else
        {
            Debug.Log("The key " + skillName + "nameOfCat" + catInfo.playerIndex + " does not exist");
            skillLevel = skillLevelOnStart;
        }

        SetSkillLevel(skillLevel);
    }

    private void OnDisable()
    {
        buttonBuy.onClick.RemoveListener(PurchaseSkillUpgrade);
    }
}