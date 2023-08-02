using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRestoreInfo : MonoBehaviour
{
    [SerializeField] private PassivePowerUp passivePowerUp;
    [SerializeField] private GameObject icoToilet;
    [SerializeField] private GameObject icoFood;
    [SerializeField] private GameObject icoNoToilet;
    [SerializeField] private GameObject icoNoFood;
    [SerializeField] private GameObject textDefaultRestore;
    [SerializeField] private GameObject textRestore1;
    [SerializeField] private GameObject textRestore2;
    private bool isFood;
    private bool isToiletClean;

    void Start()
    {
        if (PassivePowerUp.FoodSpeeUp == true || PassivePowerUp.Food2SpeeUp == true || PassivePowerUp.Food3SpeeUp == true)
        {
            icoFood.SetActive(true);
            icoNoFood.SetActive(false);
            isFood = true;
        }
        else
        {
            icoFood.SetActive(false);
            icoNoFood.SetActive(true);
            isFood = false;
        }

        if (PassivePowerUp.ToiletSpeeUp == true || PassivePowerUp.Toilet2SpeeUp == true || PassivePowerUp.Toilet3SpeeUp == true)
        {
            icoToilet.SetActive(true);
            icoNoToilet.SetActive(false);
            isToiletClean = true;
        }
        else
        {
            icoToilet.SetActive(false);
            icoNoToilet.SetActive(true);
            isToiletClean = false;
        }

        if (!isFood && !isToiletClean)
        {
            textDefaultRestore.SetActive(true);
        }
        else if (!isFood || !isToiletClean)
        {
            textRestore1.SetActive(true);
        }
        else if (isFood && isToiletClean)
        {
            textRestore2.SetActive(true);
        }
    }

}
