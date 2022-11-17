using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIco3 : MonoBehaviour, IClickable
{
    public GameObject foodButtonYes;
    public Button foodButton;
    public void Click()
    {
        foodButton.interactable = false;
        foodButtonYes.SetActive(true);
    }



}
