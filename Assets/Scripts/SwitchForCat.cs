using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchForCat : MonoBehaviour
{
    private GameObject catObj1;
    private GameObject catObj2;
    private GameObject catObj3;
    private GameObject catObj4;
    private GameObject catObj5;
    private GameObject catObj6;
    private GameObject catObj7;
    private GameObject catObj8;
    private GameObject catObj9;
    private GameObject catObj10;
    private GameObject catObj11;
    private GameObject catObj12;
    private GameObject catObj13;
    private GameObject catObj14;
    private GameObject catObj15;
    private int catThatChoosed;
    void Start()
    {
        Time.timeScale = 1;
        catThatChoosed = PlayerPrefs.GetInt("Player");
        catObj1 = gameObject.transform.GetChild(0).gameObject;
        catObj2 = gameObject.transform.GetChild(1).gameObject;
        catObj3 = gameObject.transform.GetChild(2).gameObject;
        catObj4 = gameObject.transform.GetChild(3).gameObject;
        catObj5 = gameObject.transform.GetChild(4).gameObject;
        catObj6 = gameObject.transform.GetChild(5).gameObject;
        catObj7 = gameObject.transform.GetChild(6).gameObject;
        catObj8 = gameObject.transform.GetChild(7).gameObject;
        catObj9 = gameObject.transform.GetChild(8).gameObject;
        catObj10 = gameObject.transform.GetChild(9).gameObject;
        catObj11 = gameObject.transform.GetChild(10).gameObject;
        catObj12 = gameObject.transform.GetChild(11).gameObject;
        catObj13 = gameObject.transform.GetChild(12).gameObject;
        catObj14 = gameObject.transform.GetChild(13).gameObject;
        catObj15 = gameObject.transform.GetChild(14).gameObject;
        switch (catThatChoosed)
        {
            case 0:
                PlayerPrefs.SetString("CatInShopActive", "blackfatcat");
                catObj1.SetActive(true);
                break;
            case 1:
                PlayerPrefs.SetString("CatInShopActive", "greyfatcat");
                catObj11.SetActive(true);                
                break;
            case 2:
                PlayerPrefs.SetString("CatInShopActive", "whitefatcat");
                catObj3.SetActive(true);              
                break;
            case 3:               
                PlayerPrefs.SetString("CatInShopActive", "brownfatcat");
                catObj13.SetActive(true);
                break;
            case 4:
                PlayerPrefs.SetString("CatInShopActive", "tigerfatcat");
                catObj5.SetActive(true);               
                break;
            case 5:
                PlayerPrefs.SetString("CatInShopActive", "beigefatcat");
                catObj6.SetActive(true);               
                break;
            case 6:
                PlayerPrefs.SetString("CatInShopActive", "DSOSlimCat");
                catObj4.SetActive(true);               
                break;
            case 7:
                PlayerPrefs.SetString("CatInShopActive", "3BSlimCat");
                catObj15.SetActive(true);               
                break;
            case 8:
                PlayerPrefs.SetString("CatInShopActive", "BSlimCat");
                catObj2.SetActive(true);                
                break;
            case 9:
                PlayerPrefs.SetString("CatInShopActive", "DGSlimCat");
                catObj7.SetActive(true);
                break;
            case 10:
                PlayerPrefs.SetString("CatInShopActive", "GSlimCat");
                catObj14.SetActive(true);               
                break;
            case 11:                
                PlayerPrefs.SetString("CatInShopActive", "RSlimCat");
                catObj8.SetActive(true);
                break;
            case 12:                
                PlayerPrefs.SetString("CatInShopActive", "SSlimCat");
                catObj9.SetActive(true);
                break;
            case 13:                
                PlayerPrefs.SetString("CatInShopActive", "SOSlimCat");
                catObj12.SetActive(true);
                break;
            case 14:                
                PlayerPrefs.SetString("CatInShopActive", "SlimCat");
                catObj10.SetActive(true);
                break;
            default:                
                PlayerPrefs.SetString("CatInShopActive", "blackfatcat");
                catObj1.SetActive(true);
                break;
        }
    }
   
    public void SwitchCats(int Cat)
    {
        switch (Cat)
        {
            case 1:
                PlayerPrefs.SetString("CatInShopActive", "blackfatcat");
                catObj1.SetActive(true);               
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 2:
                catObj1.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "BSlimCat");
                catObj2.SetActive(true);                
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 3:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "whitefatcat");
                catObj3.SetActive(true);                
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 4:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "DSOSlimCat");
                catObj4.SetActive(true);                
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 5:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "tigerfatcat");
                catObj5.SetActive(true);               
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 6:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "beigefatcat");
                catObj6.SetActive(true);                
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 7:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "DGSlimCat");
                catObj7.SetActive(true);                
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 8:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "RSlimCat");
                catObj8.SetActive(true);                
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 9:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "SSlimCat");
                catObj9.SetActive(true);               
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 10:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "SlimCat");
                catObj10.SetActive(true);                
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 11:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "greyfatcat");
                catObj11.SetActive(true);               
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 12:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "SOSlimCat");
                catObj12.SetActive(true);                
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 13:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "brownfatcat");
                catObj13.SetActive(true);                
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
            case 14:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "GSlimCat");
                catObj14.SetActive(true);                
                catObj15.SetActive(false);
                break;
            case 15:
                catObj1.SetActive(false);
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                PlayerPrefs.SetString("CatInShopActive", "3BSlimCat");
                catObj15.SetActive(true);                
                break;
            default:
                PlayerPrefs.SetString("CatInShopActive", "blackfatcat");
                catObj1.SetActive(true);                
                catObj2.SetActive(false);
                catObj3.SetActive(false);
                catObj4.SetActive(false);
                catObj5.SetActive(false);
                catObj6.SetActive(false);
                catObj7.SetActive(false);
                catObj8.SetActive(false);
                catObj9.SetActive(false);
                catObj10.SetActive(false);
                catObj11.SetActive(false);
                catObj12.SetActive(false);
                catObj13.SetActive(false);
                catObj14.SetActive(false);
                catObj15.SetActive(false);
                break;
        }
    }
    public void hideCats () {
        catObj1.transform.GetChild(0).gameObject.SetActive(false);
        catObj2.transform.GetChild(0).gameObject.SetActive(false);
        catObj3.transform.GetChild(0).gameObject.SetActive(false);
        catObj4.transform.GetChild(0).gameObject.SetActive(false);
        catObj5.transform.GetChild(0).gameObject.SetActive(false);
        catObj6.transform.GetChild(0).gameObject.SetActive(false);
        catObj7.transform.GetChild(0).gameObject.SetActive(false);
        catObj8.transform.GetChild(0).gameObject.SetActive(false);
        catObj9.transform.GetChild(0).gameObject.SetActive(false);
        catObj10.transform.GetChild(0).gameObject.SetActive(false);
        catObj11.transform.GetChild(0).gameObject.SetActive(false);
        catObj12.transform.GetChild(0).gameObject.SetActive(false);
        catObj13.transform.GetChild(0).gameObject.SetActive(false);
        catObj14.transform.GetChild(0).gameObject.SetActive(false);
        catObj15.transform.GetChild(0).gameObject.SetActive(false);
    }
        public void showCats () {
        catObj1.transform.GetChild(0).gameObject.SetActive(true);
        catObj2.transform.GetChild(0).gameObject.SetActive(true);
        catObj3.transform.GetChild(0).gameObject.SetActive(true);
        catObj4.transform.GetChild(0).gameObject.SetActive(true);
        catObj5.transform.GetChild(0).gameObject.SetActive(true);
        catObj6.transform.GetChild(0).gameObject.SetActive(true);
        catObj7.transform.GetChild(0).gameObject.SetActive(true);
        catObj8.transform.GetChild(0).gameObject.SetActive(true);
        catObj9.transform.GetChild(0).gameObject.SetActive(true);
        catObj10.transform.GetChild(0).gameObject.SetActive(true);
        catObj11.transform.GetChild(0).gameObject.SetActive(true);
        catObj12.transform.GetChild(0).gameObject.SetActive(true);
        catObj13.transform.GetChild(0).gameObject.SetActive(true);
        catObj14.transform.GetChild(0).gameObject.SetActive(true);
        catObj15.transform.GetChild(0).gameObject.SetActive(true);
    }
}