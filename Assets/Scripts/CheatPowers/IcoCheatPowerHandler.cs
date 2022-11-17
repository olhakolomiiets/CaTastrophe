using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoCheatPowerHandler : MonoBehaviour
{
    private GameObject cheatPowerIcon; 
    public GameObject[] cheatPowers; 

    private void OnEnable() 
    {
    
    }

    public void GetIconAndOpen(GameObject ico) 
    {
        cheatPowerIcon = ico;
        cheatPowerIcon.GetComponent<CheatIco>().icoAnimator.enabled = true;
        this.gameObject.SetActive(true);
    }

    public void SetIcon (int powerId) 
    {   
         int activePowerId = cheatPowerIcon.GetComponent<CheatIco>().FirstActivePower();  
         Debug.Log("activePowerId " + activePowerId + " powerId  " + powerId);  
        if(activePowerId == 6)
        {    
            ClearAllIcons();
            string nameCat = PlayerPrefs.GetString("CatInShopActive");
        
            cheatPowerIcon.transform.GetChild(powerId).gameObject.SetActive(true);
            cheatPowers[powerId].transform.GetChild(1).gameObject.SetActive(true);
            cheatPowers[powerId].transform.GetChild(6).gameObject.SetActive(true);  
                
            CheatPower _cheatPower =  cheatPowers[powerId].GetComponent<CheatPower>();
            string powerName = _cheatPower.ppNameCheatPower;       
            PlayerPrefs.SetInt(nameCat + powerName, 2);
            Debug.Log(" nameCat " + nameCat + "powerName - " + powerName + " save -- 2");
            int icoID = cheatPowerIcon.GetComponent<CheatIco>().icoID;
            PlayerPrefs.SetInt(nameCat + "icoSave" + icoID, powerId + 1);
        }
        else 
        {
            RemoveIcon(activePowerId); 
            ClearAllIcons();  
            string nameCat = PlayerPrefs.GetString("CatInShopActive");

            CheatPower _cheatActivePower =  cheatPowers[activePowerId].GetComponent<CheatPower>();
            string powerActiveName = _cheatActivePower.ppNameCheatPower;       
            PlayerPrefs.SetInt(nameCat + powerActiveName, 1);
            Debug.Log(" nameCat " + nameCat + "powerActiveName - " + powerActiveName + " save -- 1");
            cheatPowers[activePowerId].transform.GetChild(0).gameObject.SetActive(true);     
            cheatPowers[activePowerId].transform.GetChild(1).gameObject.SetActive(false);
            cheatPowers[activePowerId].transform.GetChild(6).gameObject.SetActive(false); 

            cheatPowerIcon.transform.GetChild(powerId).gameObject.SetActive(true);
            cheatPowers[powerId].transform.GetChild(1).gameObject.SetActive(true);
            cheatPowers[powerId].transform.GetChild(6).gameObject.SetActive(true);
            CheatPower _cheatPower =  cheatPowers[powerId].GetComponent<CheatPower>();
            string powerName = _cheatPower.ppNameCheatPower;       
            PlayerPrefs.SetInt(nameCat + powerName, 2);
            Debug.Log(" nameCat " + nameCat + "powerName - " + powerName + " save -- 2");

            int icoID = cheatPowerIcon.GetComponent<CheatIco>().icoID;
            PlayerPrefs.SetInt(nameCat + "icoSave" + icoID, powerId + 1); 
        }
            cheatPowerIcon.GetComponent<CheatIco>().icoAnimator.enabled = false;
            this.gameObject.SetActive(false);
    }

    public void RemoveIcon (int powerId) 
    { 
        int activePowerId = cheatPowerIcon.GetComponent<CheatIco>().FirstActivePower();  
        if(activePowerId == powerId)
        {    
        ClearAllIcons();
        int icoID = cheatPowerIcon.GetComponent<CheatIco>().icoID;
        string nameCat = PlayerPrefs.GetString("CatInShopActive");
        CheatPower _cheatPower =  cheatPowers[powerId].GetComponent<CheatPower>();
        string powerName = _cheatPower.ppNameCheatPower;
        PlayerPrefs.SetInt(nameCat + "icoSave" + icoID, 0);
        PlayerPrefs.SetInt(nameCat + powerName, 1);
        Debug.Log(" nameCat " + nameCat + "powerName - " + powerName + " save -- 1");   
        cheatPowers[powerId].transform.GetChild(1).gameObject.SetActive(false);
        cheatPowers[powerId].transform.GetChild(0).gameObject.SetActive(true);
        //Change remove/select buttons
        cheatPowers[powerId].transform.GetChild(5).gameObject.SetActive(true);
        cheatPowers[powerId].transform.GetChild(6).gameObject.SetActive(false);
        }
    }

    public void ClearAllIcons() 
    { 
        cheatPowerIcon.transform.GetChild(0).gameObject.SetActive(false);
        cheatPowerIcon.transform.GetChild(1).gameObject.SetActive(false);
        cheatPowerIcon.transform.GetChild(2).gameObject.SetActive(false);
        cheatPowerIcon.transform.GetChild(3).gameObject.SetActive(false);
        cheatPowerIcon.transform.GetChild(4).gameObject.SetActive(false);
    }

     public void CloseShop() 
    {
        cheatPowerIcon.GetComponent<CheatIco>().icoAnimator.enabled = false;
        this.gameObject.SetActive(false);
    }
}
