using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatsPromo : MonoBehaviour
{
    [SerializeField] private GameObject promoWindow;
    [SerializeField] private AllCatsID allCatsID;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("CatStoreFirstMessages") == 1)
        {
            if (PlayerPrefs.GetInt("HatsPromo2024") == 0)
            {
                promoWindow.SetActive(true);
                PlayerPrefs.SetInt("HatsPromo2024", 1);
                SetHatIsBoughtToAllCats();
            }
        }
    }

    void SetHatIsBoughtToAllCats()
    {
        foreach (string catID in allCatsID.allCatsID)
        {
            PlayerPrefs.SetInt(catID + "Hat24", 1);
            PlayerPrefs.SetInt(catID + "Hat25", 1);
        }
    }
}