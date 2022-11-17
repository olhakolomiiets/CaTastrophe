using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buySand : MonoBehaviour, IClickable
{
    public string ppnamePower;
    [SerializeField] private int price;
    [SerializeField] private int quantity;
    private int TotalScore;
    [SerializeField] private GameObject noMoneyTag;


    private void Awake()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        noMoneyTag.SetActive(false);
    }

    void Start()
    {

        TotalScore = PlayerPrefs.GetInt("TotalScore");

    }
    public void Click()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (TotalScore >= price)
        {
            PlayerPrefs.SetInt(ppnamePower, PlayerPrefs.GetInt(ppnamePower) + quantity);
            TotalScore = TotalScore - price;
            SoundManager.snd.PlaybuySounds();
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }

        else if (TotalScore < price)
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
