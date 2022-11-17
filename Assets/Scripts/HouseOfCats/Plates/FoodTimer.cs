using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTimer : MonoBehaviour, IClickable
{
    public float msFoodTime = 20000.0f;
    public string nameFoodForPrefTime;
    public Text foodTimer;
    public GameObject foodButtonYes;
    public Button foodButton;
    private ulong foodAdded;
    private ulong exitTime;
    private float powerPerSec;
    private float totalEnergy;
    public EnergyCat[] energyCats;
    public PlatesQueueAll platesQ;
    public PowerPointManager powerPoints;
    public float pointsBoostWhenExit;
    public FoodIco foodIco;
    public float secondsLeftFood;
    public float pointsBoostWhenWereExit;
    public int level;

    void Start()
    {
        Load();
        platesQ = base.GetComponentInParent<PlatesQueueAll>();
        foodButton = GetComponent<Button>();
        if (PlayerPrefs.HasKey(nameFoodForPrefTime))
        {
            foodAdded = ulong.Parse(PlayerPrefs.GetString(nameFoodForPrefTime));
        }
        if (!IsFoodEnd())
        {
            Debug.Log("Now     " + AssetTimeStampToDate2((ulong)DateTime.Now.Ticks));
            Debug.Log("foodAddedBforeTimerStart  " + AssetTimeStampToDate2(foodAdded));
            foodButton.interactable = false;
        }
    }
    void Update()
    {
        if (!foodButton.IsInteractable())
        {
            if (IsFoodEnd())
            {
                if (platesQ.IsActiveIcons())
                {
                    foodAdded = (ulong)DateTime.Now.Ticks;
                    PlayerPrefs.SetString(nameFoodForPrefTime, DateTime.Now.Ticks.ToString());
                }
                else
                {
                    foreach (EnergyCat energyCat in energyCats)
                    {
                        energyCat.foodSpeeUp = false;
                    }
                    foodButton.interactable = true;
                    foodButtonYes.SetActive(false);
                    return;
                }
                PlayerPrefs.SetFloat("SecondsLeftFood", secondsLeftFood);
            }
            // Set the Timer            
            ulong diff = ((ulong)DateTime.Now.Ticks - foodAdded);
            // Debug.Log("foodAddedDate In Timer Start  " + AssetTimeStampToDate2(foodAdded));
            // Debug.Log("Now In Timer Start  " + AssetTimeStampToDate2((ulong)DateTime.Now.Ticks));
            ulong ms = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msFoodTime - ms) / 1000.0f;
            // StartCoroutine(WaitAndPrint(secondsLeft));
            foodButtonYes.SetActive(true);
            foreach (EnergyCat energyCat in energyCats)
            {
                energyCat.foodSpeeUp = true;
            }
            secondsLeftFood = secondsLeft;
            //  Debug.Log("secondsLeftFood inside loop " + secondsLeftFood);
            string r = "";
            // Hours
            // r += ((int)secondsLeft / 3600).ToString() + "h ";
            // secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //Minutes
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //Sec
            r += (secondsLeft % 60).ToString("00") + "s "; ;
            foodTimer.text = r;
        }
    }
    // private IEnumerator WaitAndPrint(float waitTime)
    // {
       
    //       yield return new WaitForSeconds(1f);
    //         Debug.Log("secondsLeft in food Timer " + (int)waitTime);
            
    // }
    public void Click()
    {
        if (IsFoodEnd())
        {
            foodAdded = (ulong)DateTime.Now.Ticks;
            PlayerPrefs.SetString(nameFoodForPrefTime, DateTime.Now.Ticks.ToString());
            foodButton.interactable = false;
            foodButtonYes.SetActive(true);
        }
    }
    DateTime AssetTimeStampToDate2(ulong assetTimeStamp)
    {
        long assetTimeStampLong = Convert.ToInt64(assetTimeStamp);
        DateTime time = new DateTime((long)assetTimeStampLong);
        DateTime localTime = time.ToLocalTime();
        DateTime UTCTime = localTime.ToUniversalTime();
        return UTCTime;
    }
    private bool IsFoodEnd()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - foodAdded);
        ulong ms = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msFoodTime - ms) / 1000.0f;
        if (secondsLeft < 0)
        {
            foodTimer.text = "";
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("ExitTime"))
        {
            exitTime = ulong.Parse(PlayerPrefs.GetString("ExitTime"));
        }
        if (PlayerPrefs.HasKey("SecondsLeftFood"))
        {
            secondsLeftFood = PlayerPrefs.GetFloat("SecondsLeftFood");
        }
        ulong diffExit = ((ulong)DateTime.Now.Ticks - exitTime);
        ulong msExit = diffExit / TimeSpan.TicksPerMillisecond;
        float secondsAfterExit = (float)msExit / 1000.0f;
        Debug.Log("secondsLeftFood!!!!!!!!!!!! " + (int)secondsLeftFood);
        Debug.Log("secondsLeftExit!!!!!!!!!!!! " + (int)secondsAfterExit);

        if (secondsAfterExit < secondsLeftFood)
        {
            Debug.Log(" secondsAfterExit < secondsLeftFood ");
            pointsBoostWhenWereExit = (int)secondsAfterExit * 0.03333334f;
            if (pointsBoostWhenWereExit > 0)
            {
                powerPoints.pointsWhenYouWereAbsent = powerPoints.pointsWhenYouWereAbsent + pointsBoostWhenWereExit;
                Debug.Log("Points when you was absent Timer NotFinished " + pointsBoostWhenWereExit);
            }
        }
        if (secondsLeftFood < secondsAfterExit)
        {
            if ((int)secondsLeftFood == 0)
            {
                pointsBoostWhenWereExit = (int)secondsAfterExit * 0.01666667f;
                Debug.Log("  secondsLeftFood < secondsAfterExit  " + this.gameObject.name);
            }
            else if ((int)secondsLeftFood > 0)
            {
                float pointsBoostWhenWereExitFood = (int)secondsLeftFood * 0.03333334f;
                float exitDifference = secondsAfterExit - secondsLeftFood;
                float pointsBoostWhenWereExitNoFood = exitDifference * 0.01666667f;
                pointsBoostWhenWereExit = pointsBoostWhenWereExitFood + pointsBoostWhenWereExitNoFood;
                Debug.Log("  secondsLeftFood > 0  ");
                Debug.Log("pointsBoostWhenWereExit = pointsBoostWhenWereExitFood + pointsBoostWhenWereExitNoFood"
                + pointsBoostWhenWereExit + "= " + pointsBoostWhenWereExitFood + "+ " + pointsBoostWhenWereExitNoFood);
            }
            if (pointsBoostWhenWereExit > 0)
            {
                powerPoints.pointsWhenYouWereAbsent = powerPoints.pointsWhenYouWereAbsent + pointsBoostWhenWereExit;
                Debug.Log("Points when you was absent Timer Left or 0 " + pointsBoostWhenWereExit);
            }
        }
        if (secondsAfterExit < secondsLeftFood)
        {
            secondsLeftFood = secondsLeftFood - secondsAfterExit;
            float secondsForFoodTimer = secondsLeftFood;
            if (secondsLeftFood > msFoodTime / 1000.0f)
            {
                Debug.Log("Set1Ico >20  secondsLeftFood= " + secondsLeftFood);
                platesQ.foodIcons[0].foodButtonYes.SetActive(true);
                platesQ.foodIcons[0].foodButton.interactable = false;
                secondsForFoodTimer = secondsLeftFood - msFoodTime / 1000.0f;
            }
            if (level >= 2)
            {
                if (secondsLeftFood > (msFoodTime / 1000.0f) * 2)
                {
                    Debug.Log("Set2Ico >40  secondsLeftFood= " + secondsLeftFood);
                    platesQ.foodIcons[1].foodButtonYes.SetActive(true);
                    platesQ.foodIcons[1].foodButton.interactable = false;
                    secondsForFoodTimer = secondsLeftFood - (msFoodTime / 1000.0f) * 2;
                }
                if (level == 3)
                {
                    if (secondsLeftFood > (msFoodTime / 1000.0f) * 3)
                    {
                        Debug.Log("Set3Ico >60  secondsLeftFood= " + secondsLeftFood);
                        platesQ.foodIcons[2].foodButtonYes.SetActive(true);
                        platesQ.foodIcons[2].foodButton.interactable = false;
                        secondsForFoodTimer = secondsLeftFood - (msFoodTime / 1000.0f) * 3;
                    }
                }
            }
            Debug.Log("secondsForFoodTimer------- " + secondsForFoodTimer);
            DateTime now = AssetTimeStampToDate2((ulong)DateTime.Now.Ticks);
            Debug.Log("Now is " + now);
            DateTime nowPlusSeconds = now.AddSeconds(-secondsForFoodTimer);
            ulong foodAddedWithSeconds = ((ulong)nowPlusSeconds.Ticks);
            DateTime theDateAfterAll = AssetTimeStampToDate2(foodAddedWithSeconds);
            Debug.Log("Now - seconds " + theDateAfterAll);
            foodAdded = foodAddedWithSeconds;
            PlayerPrefs.SetString(nameFoodForPrefTime, foodAdded.ToString());
        }
        else secondsLeftFood = 0;
        Debug.Log("secondsLeftFood Afret Check!!!!!!!!!!!! " + secondsLeftFood);
        Debug.Log("LooooooaaaaddddAlllllllLLLLLLLLLLLllllllllllllllllllll ");
    }
    public void Save()
    {
        PlayerPrefs.SetString("ExitTime", DateTime.Now.Ticks.ToString());
        SaveTimeCounterLeft();
        PlayerPrefs.Save();
        Debug.Log("SaveAlllllllLLLLLLLLLLLllllllllllllllllllll ");

    }
    void OnApplicationQuit()
    {
        Save();
        Debug.Log("OnApplicationQuit!!!!!!!!!!!");
    }
    private void SaveTimeCounterLeft()
    {
        Debug.Log("secondsLeftFood before adding ico when saving = " + secondsLeftFood);
        if (platesQ.HowManyActiveFood() == 3)
        {
            secondsLeftFood = secondsLeftFood + ((msFoodTime / 1000.0f) * 3);
            Debug.Log("secondsLeftFood + 3 ico " + secondsLeftFood);
        }
        if (platesQ.HowManyActiveFood() == 2)
        {
            secondsLeftFood = secondsLeftFood + ((msFoodTime / 1000.0f) * 2);
            Debug.Log("secondsLeftFood + 2 ico " + secondsLeftFood);
        }
        if (platesQ.HowManyActiveFood() == 1)
        {
            secondsLeftFood = secondsLeftFood + (msFoodTime / 1000.0f);
            Debug.Log("secondsLeftFood + 1 ico " + secondsLeftFood);
        }
        if (platesQ.HowManyActiveFood() == 0)
        {

        }
        Debug.Log("secondsLeftFood Alllll to save " + secondsLeftFood);
        PlayerPrefs.SetFloat("SecondsLeftFood", secondsLeftFood);

    }
}