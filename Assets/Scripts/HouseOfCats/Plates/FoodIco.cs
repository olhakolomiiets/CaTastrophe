using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIco : MonoBehaviour
{
    public GameObject foodButtonYes;
    public Button foodButton;
    public bool isActive;

    public void IconOn()
    {
        // foodButton.interactable = false;
        foodButtonYes.SetActive(true);
        isActive = true;
    }
    public void IconOff()
    {
        // foodButton.interactable = true;
        foodButtonYes.SetActive(false);
        isActive = false;
    }
    
}
