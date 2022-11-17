using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class BuyCheatPower : MonoBehaviour
{
    public bool isPurchased;
    [SerializeField] private string ppNameCheatPower;
    [SerializeField] private CheatPowersHandler _cheatPowerHandler;
    private string ppNameCat;
    public int price;
    public int purch;
    private int TotalScore;
    public GameObject buyTxt;
    public GameObject alreadyBoughtTxt;
    [Header("Tags")]
    [SerializeField] private GameObject priceTag;
    [SerializeField] private GameObject doneTag;
    [SerializeField] private GameObject noMoneyTag;
    [SerializeField] private Text priceText;

    // public delegate void DelegateCheatPowerBought();
    // public static event DelegateCheatPowerBought CheatPowerBoughtEvent;
    // public static event Action CheatPowerBought = delegate { };
    private void Awake()
    {
        ppNameCat = PlayerPrefs.GetString("CatInShopActive");
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        priceText.text = price.ToString();
        noMoneyTag.SetActive(false);
        purch = PlayerPrefs.GetInt(ppNameCat + ppNameCheatPower, 0);  

        if (purch == 0)
        {
            priceTag.SetActive(true);
            buyTxt.SetActive(true);
            doneTag.SetActive(false);
            alreadyBoughtTxt.SetActive(false);
            isPurchased = false;
        }
        else
        {
            isPurchased = true;
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
            isPurchased = true;
        }
    }
    void OnEnable()
    {
        Awake();
    }
    public void buy()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        // ppNameCat = PlayerPrefs.GetString("CatInShopActive");
        if (TotalScore >= price && isPurchased == false)
        {
            PlayerPrefs.SetInt(ppNameCat + ppNameCheatPower, 1);
            TotalScore = TotalScore - price;
            isPurchased = true;
            SoundManager.snd.PlaybuySounds();
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            _cheatPowerHandler.UpdateAllIcons();
            this.gameObject.SetActive(false);
            // if(CheatPowerBoughtEvent != null)
            // CheatPowerBoughtEvent?.Invoke();
            // CheatPowerBought();
        }
        else if (isPurchased == false)
        {
            StartCoroutine(NoMoney());
        }
    }
    IEnumerator NoMoney()
    {
        noMoneyTag.SetActive(true);
        yield return new WaitForSeconds(1f);
        noMoneyTag.SetActive(false);
    }
}