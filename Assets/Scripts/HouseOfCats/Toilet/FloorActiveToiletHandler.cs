﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActiveToiletHandler : MonoBehaviour
{
    [SerializeField] private GameObject toilet2Floor;
    [SerializeField] private GameObject toilet3Floor;
    public string PrefForFloor2;
    public string PrefForFloor3;
    public delegate void ToiletActiveFloorDelegate();
    public static event ToiletActiveFloorDelegate ToiletActiveFloorEvent;
    
    private void Awake() 
    {
       UpdateActiveToilets();   
    }
    private void OnEnable() 
    {
        BuyFloorHandler.FloorBought += UpdateActiveToilets;
    }

    private void UpdateActiveToilets() 
    {
        if (PlayerPrefs.GetInt(PrefForFloor2, 0) == 1)
        {
            toilet2Floor.SetActive(true);
        }
        if (PlayerPrefs.GetInt(PrefForFloor3, 0) == 1)
        {
            toilet3Floor.SetActive(true);
        }          
    }
}
