using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyRestoreInfo : MonoBehaviour
{
    [SerializeField] private PassivePowerUp passivePowerUp;
    [SerializeField] private GameObject icoToilet;
    [SerializeField] private GameObject icoFood;
    [SerializeField] private GameObject icoNoToilet;
    [SerializeField] private GameObject icoNoFood;
    [SerializeField] private Text textTotalRestore;
    [SerializeField] private Text textRestoreFood;
    [SerializeField] private Text textRestoreToilet;


    void Start()
    {
        CheckFoodAndToilet();

    }


    private void CheckFoodAndToilet()
    {
        int trueCount = 0;
        int toiletCount = 0;
        int foodCount = 0;

        if (PassivePowerUp.FoodSpeeUp)
        {
            trueCount++;
            foodCount++;
        }           

        if (PassivePowerUp.Food2SpeeUp)
        {
            trueCount++;
            foodCount++;
        }

        if (PassivePowerUp.Food3SpeeUp)
        {
            trueCount++;
            foodCount++;
        }

        if (PassivePowerUp.ToiletSpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        if (PassivePowerUp.Toilet2SpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        if (PassivePowerUp.Toilet3SpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        switch (trueCount)
        {
            case 1:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"1.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 2:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"2"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 3:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"2.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 4:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"3"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 5:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"3.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 6:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"4"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            default:
                textTotalRestore.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("nowAllCatsRestoring")} {"1"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

        }

        switch (foodCount)
        {
            case 1:
                textRestoreFood.text = $"{"+ 0.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 2:
                textRestoreFood.text = $"{"+ 1"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 3:
                textRestoreFood.text = $"{"+ 1.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            default:
                textRestoreFood.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("catsHungry")}";
                break;
        }

        switch (toiletCount)
        {
            case 1:
                textRestoreToilet.text = $"{"+ 0.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 2:
                textRestoreToilet.text = $"{"+ 1"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            case 3:
                textRestoreToilet.text = $"{"+ 1.5"} {Lean.Localization.LeanLocalization.GetTranslationText("energyPer20min")}";
                break;

            default:
                textRestoreToilet.text = $"{Lean.Localization.LeanLocalization.GetTranslationText("toiletsDirty")}";
                break;
        }

        if (PassivePowerUp.FoodSpeeUp == true || PassivePowerUp.Food2SpeeUp == true || PassivePowerUp.Food3SpeeUp == true)
        {
            icoFood.SetActive(true);
            icoNoFood.SetActive(false);
        }
        else
        {
            icoFood.SetActive(false);
            icoNoFood.SetActive(true);
        }

        if (PassivePowerUp.ToiletSpeeUp == true || PassivePowerUp.Toilet2SpeeUp == true || PassivePowerUp.Toilet3SpeeUp == true)
        {
            icoToilet.SetActive(true);
            icoNoToilet.SetActive(false);
        }
        else
        {
            icoToilet.SetActive(false);
            icoNoToilet.SetActive(true);
        }

    }
}
