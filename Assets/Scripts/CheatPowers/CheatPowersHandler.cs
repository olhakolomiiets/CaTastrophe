using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPowersHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> cheatPowers;
    private int purch;
    [SerializeField] private GameObject[] cheatPowersInfo;
    [SerializeField] private GameObject cheatPowersTip;
    public bool isCheckHat;

    void Start()
    {
        // BuyCheatPower.CheatPowerBought += EventTest;             
    }

    private void OnEnable()
    {
        if (isCheckHat)
        {
            CheckHat();
            return;
        }

        foreach (GameObject cheatPow in cheatPowers)
        {
            CheatPower _cheatPower = cheatPow.GetComponent<CheatPower>();
            string powerName = _cheatPower.ppNameCheatPower;
            string nameCat = PlayerPrefs.GetString("CatInShopActive");
            purch = PlayerPrefs.GetInt(nameCat + powerName);

            if (purch == 0)
            {
                _cheatPower.icoDefault.SetActive(true);
                _cheatPower.icoIsNotBought.SetActive(true);
                _cheatPower.icoSelect.SetActive(false);
                _cheatPower.buttonSelect.SetActive(false);
                _cheatPower.buttonRemove.SetActive(false);
            }
            else if (purch == 2)
            {
                _cheatPower.icoDefault.SetActive(false);
                _cheatPower.icoIsNotBought.SetActive(false);
                _cheatPower.icoSelect.SetActive(true);
                _cheatPower.buttonSelect.SetActive(false);
                _cheatPower.buttonRemove.SetActive(true);
                // Add button to cansel power only if came from current ico       
            }
            else
            {
                _cheatPower.icoDefault.SetActive(true);
                _cheatPower.icoIsNotBought.SetActive(false);
                _cheatPower.icoSelect.SetActive(false);
                _cheatPower.buttonSelect.SetActive(true);
                _cheatPower.buttonRemove.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        // BuyCheatPower.CheatPowerBought -= EventTest;
    }

    public void ShowTip()
    {
        if (PlayerPrefs.GetInt("ShowCheatPowerSetTip") == 0)
        {
            cheatPowersTip.SetActive(true);
            PlayerPrefs.SetInt("ShowCheatPowerSetTip", 1);
        }
    }

    public void ResetCheatPowers()
    {
        foreach (GameObject cheatPow in cheatPowers)
        {
            CheatPower _cheatPower = cheatPow.GetComponent<CheatPower>();
            string powerName = _cheatPower.ppNameCheatPower;
            string nameCat = PlayerPrefs.GetString("CatInShopActive");
            PlayerPrefs.SetInt(nameCat + powerName, 0);
        }
        OnEnable();
    }

    public void UpdateAllIcons()
    {
        OnEnable();
    }

    public void CheatPowersSwitch(int infoId)
    {
        for (int i = 0; i < cheatPowersInfo.Length; i++)
        {
            cheatPowersInfo[i].SetActive(i == infoId);
        }
    }

    private void CheckHat() 
    {
        foreach (GameObject cheatPow in cheatPowers)
        {
            CheatPower _cheatPower = cheatPow.GetComponent<CheatPower>();
            BuyCheatPower _cheatPowerInfo = _cheatPower.cheatPowerInfo;
            string powerName = _cheatPowerInfo.ppNameCheatPower;
            string nameCat = PlayerPrefs.GetString("CatInShopActive");
            
            purch = PlayerPrefs.GetInt(nameCat + powerName);

            Debug.Log("----------------- nameCat " + nameCat + " powerName - " + powerName + " Get Int " + PlayerPrefs.GetInt(nameCat + powerName));

            if (purch == 0)
            {
                _cheatPower.icoDefault.SetActive(true);
                _cheatPower.icoIsNotBought.SetActive(true);
                _cheatPower.icoSelect.SetActive(false);
                _cheatPower.buttonSelect.SetActive(false);
                _cheatPower.buttonRemove.SetActive(false);
            }
            else if (purch == 2)
            {
                _cheatPower.icoDefault.SetActive(false);
                _cheatPower.icoIsNotBought.SetActive(false);
                _cheatPower.icoSelect.SetActive(true);
                _cheatPower.buttonSelect.SetActive(false);
                _cheatPower.buttonRemove.SetActive(true);
                _cheatPower.ButtonChoose.SetActive(false);
                Debug.Log("-------------------------------------------- cheatPow if (purch == 2) " + cheatPow.name);
                // Add button to cansel power only if came from current ico       
            }
            else
            {
                _cheatPower.icoDefault.SetActive(true);
                _cheatPower.icoIsNotBought.SetActive(false);
                _cheatPower.icoSelect.SetActive(false);
                _cheatPower.buttonSelect.SetActive(true);
                _cheatPower.buttonRemove.SetActive(false);
                _cheatPower.ButtonChoose.SetActive(false);
            }
        }
    }

}
