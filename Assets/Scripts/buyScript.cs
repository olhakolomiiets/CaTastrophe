using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buyScript : MonoBehaviour
{
    public bool isPurchased;
    public string ppname;
    public Text priceText;
    public int price;
    public int purch;
    public int playerIndex;
    private int TotalScore;
    public GameObject buttonTxtBuy;
    public GameObject buttonTxtSelect;
    private Text buyText;
    private GameObject priceTag;
    [SerializeField] private GameObject nameChange;
    private int allCatsYouHave;
    private int floor1Active;
    private int floor2Active;
    [SerializeField] private GameObject noSpaceTag;
    [SerializeField] private GameObject noCleanTag;

    private void OnEnable()
    {
        Start();
    }
    void Start()
    {
        PlayerPrefs.SetInt("blackfatcat", 1);
        purch = PlayerPrefs.GetInt(ppname, 0);
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        buttonTxtSelect.transform.parent.GetComponent<Image>().enabled = true;
        buyText = buttonTxtBuy.GetComponent<Text>();
        priceTag = transform.parent.GetChild(2).gameObject;
        if (purch == 0)
        {           
            priceText.text = price.ToString();
            // buttonTxt.text = Lean.Localization.LeanLocalization.GetTranslationText("Buy");
            buttonTxtBuy.SetActive(true);
            buttonTxtSelect.SetActive(false);
        }
        else
        {
            priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("catyours");
            isPurchased = true;
            buttonTxtBuy.SetActive(false);
            buttonTxtSelect.SetActive(true);
            priceTag.SetActive(false);
            if (PlayerPrefs.GetInt("Player") == playerIndex) 
            {
               buttonTxtSelect.SetActive(false);
               buttonTxtSelect.transform.parent.GetComponent<Image>().enabled = false; 
               priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("selected");
            }
            // buttonTxt.text = Lean.Localization.LeanLocalization.GetTranslationText("Select");
            // buttonTxt.text = "SELECT";
        }
    }
    public void buy()
    {
        allCatsYouHave = PlayerPrefs.GetInt("CatsIsYoursAchieve");
        floor1Active = PlayerPrefs.GetInt("firstFloorBought");
        floor2Active = PlayerPrefs.GetInt("secondFloorBought");
        if (TotalScore >= price && isPurchased == false)
        {
            if (allCatsYouHave < 5 && floor1Active == 0 && floor2Active == 0 ||
                allCatsYouHave < 10 && floor1Active == 1 && floor2Active == 0 ||
                allCatsYouHave < 15 && floor1Active == 1 && floor2Active == 1)
            {
                if (PlayerPrefs.GetInt("AllBrokeYouCantBuyCat1") == 0 &&
                PlayerPrefs.GetInt("AllBrokeYouCantBuyCat2") == 0 &&
                PlayerPrefs.GetInt("AllBrokeYouCantBuyCat3") == 0)
                {
                    PlayerPrefs.SetInt(ppname, 1);
                    TotalScore -= price;
                    priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("catyours");
                    isPurchased = true;
                    PlayerPrefs.SetInt("TotalScore", TotalScore);
                    StartCoroutine(select());
                    buyText.text = Lean.Localization.LeanLocalization.GetTranslationText("Select");
                    priceTag.SetActive(false);
                    PlayerPrefs.SetInt("CatsIsYoursAchieve", PlayerPrefs.GetInt("CatsIsYoursAchieve") + 1);
                    nameChange.SetActive(true);

                    FirebaseAnalytics.LogEvent(name: "buy_cats", new Parameter(parameterName: "cats", parameterValue: ppname)); 
                }
                else
                {
                    StartCoroutine(NoClean());
                }
            }
            else
            {
                StartCoroutine(NoSpace());
            }
        }
        else if (isPurchased == true)
        {
            StartCoroutine(select());
        }
        else if (TotalScore < price && isPurchased == false)
        {
            StartCoroutine(noMoney());
        }
    }
    IEnumerator select()
    {
        string lastText = priceText.text;
        buttonTxtSelect.transform.parent.GetComponent<Image>().enabled = false;
        buttonTxtSelect.SetActive(false);
        buttonTxtBuy.SetActive(false);
        priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("selected");
        PlayerPrefs.SetInt("Player", playerIndex);
        yield return new WaitForSeconds(3f);
        priceText.text = lastText;
    }
    IEnumerator noMoney()
    {
        string lastText = priceText.text;
        priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("noMoney");

        yield return new WaitForSeconds(5f);
        priceText.text = lastText;
    }
    IEnumerator NoSpace()
    {
        noSpaceTag.SetActive(true);
        yield return new WaitForSeconds(3f);
        noSpaceTag.SetActive(false);
    }
    IEnumerator NoClean()
    {
        noCleanTag.SetActive(true);
        yield return new WaitForSeconds(3f);
        noCleanTag.SetActive(false);
    }
}
