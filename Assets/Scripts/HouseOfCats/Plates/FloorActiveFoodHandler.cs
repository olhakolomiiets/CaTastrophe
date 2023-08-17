using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActiveFoodHandler : MonoBehaviour
{
    [SerializeField] private GameObject plate2Floor;
    [SerializeField] private GameObject plate3Floor;
    public string PrefForFloor2;
    public string PrefForFloor3;

    public delegate void PlateActiveFloorDelegate();

    private void Awake() 
    {
       UpdateActivePlates();   
    }
    private void OnEnable() 
    {
        BuyFloorHandler.PlateActiveFloor += UpdateActivePlates;
    }

    private void UpdateActivePlates() 
    {
        if (PlayerPrefs.GetInt(PrefForFloor2, 0) == 1)
        {
            plate2Floor.SetActive(true);
        }
        if (PlayerPrefs.GetInt(PrefForFloor3, 0) == 1)
        {
            plate3Floor.SetActive(true);
        }          
    }

    private void OnDisable()
    {
        BuyFloorHandler.PlateActiveFloor -= UpdateActivePlates;
    }
}
