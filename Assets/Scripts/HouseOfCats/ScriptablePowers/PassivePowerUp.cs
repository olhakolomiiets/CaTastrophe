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
            // Debug.Log("FoodSpeeUp is " + FoodSpeeUp);
            // Debug.Log("ToiletSpeeUp is " + ToiletSpeeUp);
            foreach (FloatSO catPower in catPowersSO)
            {
                if (catPower.Value < 10)
                {
                    catPower.ChangeAmountBy(0.00083333f);
                    ValueChangeBy?.Invoke(0f);
                    if (ToiletSpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("ToiletSpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                    if (Toilet2SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("Toilet2SpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                    if (Toilet3SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("Toilet3SpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                    if (FoodSpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("FoodSpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                    if (Food2SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("Food2SpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                    if (Food3SpeeUp == true)
                    {
                        catPower.ChangeAmountBy(0.00083333f);
                        ValueChangeBy?.Invoke(0f);
                        //Debug.Log("Food3SpeeUp == true I changed catPower.Value --- " + catPower.Value + " Cat name " + catPower.nameCat);
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            //Debug.Log("Load OnOnFocus Passive AllPowers !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            LoadAllPowers();
            PlayerPrefs.SetInt("BoolForSave", 0);
        }
        else
        {
            //Debug.Log("Save OnOnFocus Passive AllPowers !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            SaveAllPowers();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllPowers();
            //Debug.Log("Save OnOnPause Passive AllPowers !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            LoadAllPowers();
            //Debug.Log("Load OnOnPause Passive AllPowers !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnDestroy()
    {
        SaveAllPowers();
        PlayerPrefs.SetInt("BoolForSave", 1);
        //Debug.Log("Save OnDestroy Passive AllPowers !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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
                //Debug.Log("Power value before load ===== " + power.Value + " to cat " + power.nameCat);
                power.ChangeAmountBy(LoadPowerWhenWasOut() * 0.00083333f);
                //Debug.Log("points Passive when Exit " + (LoadPowerWhenWasOut() * 0.00083333f));
                power.ChangeAmountBy(toiletPointsWhenWereExit);
                power.ChangeAmountBy(toiletPoints2WhenWereExit);
                power.ChangeAmountBy(toiletPoints3WhenWereExit);
                power.ChangeAmountBy(foodPointsWhenWereExit);
                power.ChangeAmountBy(foodPoints2WhenWereExit);
                power.ChangeAmountBy(foodPoints3WhenWereExit);
/*                Debug.Log("ToiletPointsWhenWereExit in POWERUP " + toiletPointsWhenWereExit);
                Debug.Log("ToiletPoints2WhenWereExit in POWERUP " + toiletPoints2WhenWereExit);
                Debug.Log("ToiletPoints3WhenWereExit in POWERUP " + toiletPoints3WhenWereExit);
                Debug.Log("FoodPointsWhenWereExit in POWERUP " + foodPointsWhenWereExit);
                Debug.Log("FoodPoints2WhenWereExit in POWERUP " + foodPoints2WhenWereExit);
                Debug.Log("FoodPoints3WhenWereExit in POWERUP " + foodPoints3WhenWereExit);
                Debug.Log("SO value after Load is ===== " + power.Value + " to cat " + power.nameCat);*/
            }
            if (power.Value >= 10)
            {
                power.SetNewAmount(10f);
            }
            if (PlayerPrefs.HasKey("firstInitPassivePowerUp") == false)
            {
                PlayerPrefs.SetInt("firstInitPassivePowerUp", 1);
                power.SetNewAmount(10f);
            }
            // Debug.Log("Load SecAfterExit !!!!!!!!!!    " + LoadPowerWhenWasOut() + " * 0.00083333f" + " = " + (LoadPowerWhenWasOut() * 0.01666667f) + " Cat is " + power.name);
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
}
