using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class PassivePowerUp : MonoBehaviour
{
    public static PassivePowerUp instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    public UnityEvent<float> ValueChangeBy = new FloatEvent();
    [SerializeField]
    public static bool FoodSpeeUp = false;
    public static bool Food2SpeeUp = false;
    public static bool Food3SpeeUp = false;
    public static bool ToiletSpeeUp = false;
    public static bool Toilet2SpeeUp = false;
    public static bool Toilet3SpeeUp = false;
    public static float toiletPointsWhenWereExit;
    public static float toiletPoints2WhenWereExit;
    public static float toiletPoints3WhenWereExit;
    public static float foodPointsWhenWereExit;
    public static float foodPoints2WhenWereExit;
    public static float foodPoints3WhenWereExit;

    public  bool FoodSpeeUp2;
    public  bool Food2SpeeUp2;
    public  bool Food3SpeeUp2;
    public  bool ToiletSpeeUp2;
    public  bool Toilet2SpeeUp2;
    public  bool Toilet3SpeeUp2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("firstInitPassivePowerUp"))
        {
            PlayerPrefs.SetInt("firstInitPassivePowerUp", 1);
            SetAllPowesToMaxOnFirstInit();
        }       
    }

    void Start()
    {
        LoadAllPowers();

        toiletPointsWhenWereExit = 0f;
        toiletPoints2WhenWereExit = 0f;
        toiletPoints3WhenWereExit = 0f;
        foodPointsWhenWereExit = 0f;
        foodPoints2WhenWereExit = 0f;
        foodPoints3WhenWereExit = 0f;
        StartCoroutine(PassivePowerUpRoutine());
    }

    private IEnumerator PassivePowerUpRoutine()
    {
        while (true)
        {
            foreach (FloatSO catPower in catPowersSO)
            {
                if (catPower.Value < 10)
                {
                    catPower.ChangeAmountBy(0.00083333f);
                    ValueChangeBy?.Invoke(0f);
                    if (ToiletSpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                    if (Toilet2SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                    if (Toilet3SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                    if (FoodSpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                    if (Food2SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                    if (Food3SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.000416665f);
                        ValueChangeBy?.Invoke(0f);
                    }
                }
            }
            FoodSpeeUp2 = FoodSpeeUp;
            Food2SpeeUp2 = Food2SpeeUp;
            Food3SpeeUp2 = Food3SpeeUp;
            ToiletSpeeUp2 = ToiletSpeeUp;
            Toilet2SpeeUp2 = Toilet2SpeeUp;
            Toilet3SpeeUp2 = Toilet3SpeeUp;
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            LoadAllPowers();
            PlayerPrefs.SetInt("BoolForSave", 0);
        }
        else
        {
            SaveAllPowers();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllPowers();
        }
        else
        {
            LoadAllPowers();
        }
    }

    private void OnDestroy()
    {
        SaveAllPowers();
        PlayerPrefs.SetInt("BoolForSave", 1);
    }

    private void SaveAllPowers()
    {
        PlayerPrefs.SetString("exitTimeForPowerPref", System.DateTime.Now.ToBinary().ToString());
        foreach (FloatSO power in catPowersSO)
        {
            if (power.Value < 10)
            {
                PlayerPrefs.SetFloat(power.name + "FloatSO", power.Value);
            }
            else PlayerPrefs.SetFloat(power.name + "FloatSO", 10f);
        }
    }

    private void LoadAllPowers()
    {
        foreach (FloatSO power in catPowersSO)
        {
            power.SetNewAmount(PlayerPrefs.GetFloat(power.name + "FloatSO"));

            if (power.Value < 10f)
            {
                power.ChangeAmountBy(LoadPowerWhenWasOut() * 0.00083333f);

                power.ChangeAmountBy(toiletPointsWhenWereExit);
                power.ChangeAmountBy(toiletPoints2WhenWereExit);
                power.ChangeAmountBy(toiletPoints3WhenWereExit);
                power.ChangeAmountBy(foodPointsWhenWereExit);
                power.ChangeAmountBy(foodPoints2WhenWereExit);
                power.ChangeAmountBy(foodPoints3WhenWereExit);
            }

            if (power.Value >= 10)
            {
                power.SetNewAmount(10f);
            }
        }
    }

    private int LoadPowerWhenWasOut()
    {
        var tempExitTime = Convert.ToInt64(PlayerPrefs.GetString("exitTimeForPowerPref"));

        var exitTimeToilet = DateTime.FromBinary(tempExitTime);
        var currentTime = DateTime.Now;
        var difference = currentTime.Subtract(exitTimeToilet);
        var rawTime = (float)difference.TotalSeconds;
        var secAfterExit = (int)rawTime;
        return secAfterExit;
    }

    public void SetAllPowestToMax()
    {
        foreach (FloatSO power in catPowersSO)
        {
            power.SetNewAmount(10f);
        }
    }

    public void SetAllPowesToMaxOnFirstInit()
    {
        foreach (FloatSO power in catPowersSO)
        {
            power.SetNewAmount(10f);
            PlayerPrefs.SetFloat(power.name + "FloatSO", 10f);           
        }
    }
}
