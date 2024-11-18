using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatsNotification : MonoBehaviour
{
    [SerializeField] private GameObject notificationIco;
    [SerializeField] private List<int> PromoItemsID;

    void OnEnable()
    {
        string nameCat = PlayerPrefs.GetString("CatInShopActive");

        if (PlayerPrefs.GetInt("FinishHatPromo") != 1)
        {
            foreach (int promoItemID in PromoItemsID)
            {              
                if (PlayerPrefs.GetInt(nameCat + "ActiveHat") != promoItemID) 
                {
                    notificationIco.SetActive(true);
                }
                else
                {
                    notificationIco.SetActive(false);
                    PlayerPrefs.SetInt("FinishHatPromo", 1);                    
                    return;
                }
            }
        }
    }
}
