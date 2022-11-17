using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buyPowersOldVersion : MonoBehaviour
{
    public bool isPurchased;
    public string ppnamePower;
    public Text priceText;
    public int price;
    public int purch;
    private int TotalScore;
    private GameObject priceTag;
    private GameObject doneTag;
    public GameObject buyTxt;
    public GameObject alreadyBoughtTxt;

    void Start()
    {

        purch = PlayerPrefs.GetInt(ppnamePower, 0);
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        priceTag = this.gameObject.transform.GetChild(3).gameObject;
        doneTag = this.gameObject.transform.GetChild(4).gameObject;

        if (purch == 0)
        {
            priceText.text = price + "$";
            priceTag.SetActive(true);
            buyTxt.SetActive(true);
            doneTag.SetActive(false);
            alreadyBoughtTxt.SetActive(false);
        }
        else
        {
            priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("alreadybought");
            isPurchased = true;
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
        }
    }
    public void buy()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (TotalScore >= price && isPurchased == false)
        {
            PlayerPrefs.SetInt(ppnamePower, 1);
            TotalScore = TotalScore - price;
            Debug.Log("TotalScore" + TotalScore);
            priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("alreadybought");
            isPurchased = true;
            buyTxt.SetActive(false);
            priceTag.SetActive(false);
            doneTag.SetActive(true);
            alreadyBoughtTxt.SetActive(true);
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
        else if (isPurchased == true)
        {
            //StartCoroutine(select());
        }
        else if (isPurchased == false)
        {
            StartCoroutine(NoMoney());
        }
    }
    IEnumerator select()
    {
        string lastText = priceText.text;
        priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("selected");
        yield return new WaitForSeconds(0.5f);
        priceText.text = lastText;
    }
    IEnumerator NoMoney()
    {
        string lastText = priceText.text;
        priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("noMoney");
        yield return new WaitForSeconds(1.5f);
        priceText.text = lastText;
    }
}
