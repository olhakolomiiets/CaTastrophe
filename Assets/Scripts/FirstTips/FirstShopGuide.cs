using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstShopGuide : MonoBehaviour
{
    public GameObject shop;
    public GameObject pillowPage;
    void Start()
    {
        var _playCount = PlayerPrefs.GetInt("HowManyGamesPlayed");

        if (_playCount >= 1 && PlayerPrefs.GetInt("FirstShop") == 0 && PlayerPrefs.GetInt("TotalScore") >= 50 && PlayerPrefs.GetInt("pillow") == 0)
        {
            shop.SetActive(true);
            StartCoroutine(PillowPage());
            PlayerPrefs.SetInt("FirstShop", 1);
        }
    }


    IEnumerator PillowPage()
    {
        yield return new WaitForSecondsRealtime(1f);
        pillowPage.SetActive(true);
    }

}
