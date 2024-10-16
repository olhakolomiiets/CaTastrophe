using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStoreTipsManager : MonoBehaviour
{
    [SerializeField] private GameObject tipCats;
    [SerializeField] private GameObject tipCatInfo;
    [SerializeField] private GameObject tipName;
    [SerializeField] private GameObject tipCheatPowers;
    [SerializeField] private GameObject tipCheatPower;
    [SerializeField] private GameObject tipHats;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject catName;
    [SerializeField] private GameObject cheatPowers;
    [SerializeField] private GameObject cheatPowerInfo;
    [SerializeField] private GameObject catInfo;


    void Start()
    {
        if (PlayerPrefs.GetInt("CatStoreFirstMessages") == 0)
        {
            tipCats.SetActive(true);
            background.SetActive(true);
            PlayerPrefs.SetInt("CatStoreFirstMessages", 1);
        }
    }

    public void ActiveTipCatInfo()
    {
        tipCats.SetActive(false);
        tipCatInfo.SetActive(true);
    }
    public void ActiveTipName()
    {
        tipCatInfo.SetActive(false);
        catName.SetActive(true);
        tipName.SetActive(true);
    }

    public void ActiveTipCheatPowers()
    {
        tipName.SetActive(false);
        catName.SetActive(false);
        tipCheatPowers.SetActive(true);
    }
    public void ActiveTipCheatPower()
    {
        tipCheatPowers.SetActive(false);
        cheatPowers.SetActive(true);
        cheatPowerInfo.SetActive(true);
        tipCheatPower.SetActive(true);
    }
    public void ActiveTipHats()
    {
        tipCheatPower.SetActive(false);
        background.SetActive(false);
        cheatPowers.SetActive(false);
        cheatPowerInfo.SetActive(false);
        tipHats.SetActive(true);
    }

    public void ExitTip()
    {
        tipCheatPower.SetActive(false);
        background.SetActive(false);
        cheatPowers.SetActive(false);
        cheatPowerInfo.SetActive(false);
        tipHats.SetActive(false);
    }
}
