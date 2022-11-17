using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCheatIcons : MonoBehaviour
{
    [SerializeField] private List<GameObject> cheatIcons; 
    // public string catName;
    private int selected;

    private void OnEnable() 
    {
       string nameCat = PlayerPrefs.GetString("CatInShopActive");
       foreach (GameObject cheatIcon in cheatIcons)
        {            
           CheatIco _cheatIco =  cheatIcon.GetComponent<CheatIco>();
           TurnOffAllIco(_cheatIco); 
           int icoID = _cheatIco.icoID;           
           selected = PlayerPrefs.GetInt(nameCat + "icoSave" + icoID); 
        //    Debug.Log("Load from pref" + "ico " + icoID + " -- " + selected + " nameCat " + nameCat);
            switch (selected)
            {
                case 0:
                    TurnOffAllIco(_cheatIco); 
                    break;
                case 1:
                    cheatIcon.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case 2:
                    cheatIcon.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case 3:
                    cheatIcon.transform.GetChild(2).gameObject.SetActive(true);
                    break;                
                case 4:
                    cheatIcon.transform.GetChild(3).gameObject.SetActive(true);
                    break;
                case 5:
                    cheatIcon.transform.GetChild(4).gameObject.SetActive(true);
                    break;
            }
        }      
    }

    public void TurnOffAllIco(CheatIco cheatIcon) 
    {        
        cheatIcon.transform.GetChild(0).gameObject.SetActive(false);
        cheatIcon.transform.GetChild(1).gameObject.SetActive(false);
        cheatIcon.transform.GetChild(2).gameObject.SetActive(false);
        cheatIcon.transform.GetChild(3).gameObject.SetActive(false);
        cheatIcon.transform.GetChild(4).gameObject.SetActive(false);    
    }
    
}
