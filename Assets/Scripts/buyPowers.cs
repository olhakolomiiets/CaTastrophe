using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buyPowers : MonoBehaviour
{
    public bool isPurchased;
    public string ppnamePower;
    public int price;
    public int purch;
    private int TotalScore;
    private GameObject priceTag;
    private GameObject doneTag;
    private GameObject noMoneyTag;
    public GameObject buyTxt;
    public GameObject alreadyBoughtTxt;
    private void Awake()
    {
        purch = PlayerPrefs.GetInt(ppnamePower, 0);
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        priceTag = this.gameObject.transform.GetChild(3).gameObject;
        doneTag = this.gameObject.transform.GetChild(4).gameObject;
        noMoneyTag = this.gameObject.transform.GetChild(5).gameObject;
        noMoneyTag.SetActive(false);

        if (purch == 0)
        {
            priceTag.SetActive(true);
            buyTxt.SetActive(true);
            doneTag.SetActive(false);
            alreadyBoughtTxt.SetActive(false);
        }
        else
        {
            isPurchased = true;
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
        }
    }
    void Start()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
    }
    public void buy()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (TotalScore >= price && isPurchased == false)
        {
            PlayerPrefs.SetInt(ppnamePower, 1);
            TotalScore = TotalScore - price;
            Debug.Log("TotalScore" + TotalScore);
            isPurchased = true;
            SoundManager.snd.PlaybuySounds();
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
            PlayerPrefs.SetInt("TotalScore", TotalScore);
            PlayerPrefs.SetInt("AwardPowersIsYours", PlayerPrefs.GetInt("AwardPowersIsYours") + 1);

            FirebaseAnalytics.LogEvent(name: "buy_power", new Parameter(parameterName: "powers" , parameterValue: ppnamePower));
        }
        else if (isPurchased == false)
        {
            StartCoroutine(NoMoney());
        }
    }
    IEnumerator NoMoney()
    {
        noMoneyTag.SetActive(true);
        yield return new WaitForSeconds(3f);
        noMoneyTag.SetActive(false);
    }
}