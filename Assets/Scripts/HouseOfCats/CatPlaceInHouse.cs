using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatPlaceInHouse : MonoBehaviour
{
    public Transform chair;
    public Transform plate;
    public Transform foodPlaceLeft;
    public Transform foodPlaceRight;
    public Transform sofa;
    public Transform house;
    public Text catName;
    public Animator animHouse;
    public string prefNamePlace;
    public int idPlace;
    public Transform drinkPlaceLeft;
    public Transform drinkPlaceRight;
    public Transform drinkPlace;
    public Transform toilet;
    public Transform ballPlace;
    public Transform sportPlace;
    void Start()
    {


    }

    void Update()
    {

    }
    public int IsPlaceBusy()
    {
        return PlayerPrefs.GetInt(prefNamePlace, 0);
    }
    public void SetPlaceBusy()
    {
        PlayerPrefs.SetInt(prefNamePlace, 1);
    }
}
