using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BrokeWhenOut : MonoBehaviour
{
    public DestructableObjectHandler[] stuffAll1Floor;
    public List<DestructableObjectHandler> stuffToDestroy1Floor = new List<DestructableObjectHandler>();
    private int secAfterExit;
    private int toiletTimer;
    private float secondsToiletLeft;
    public float msToiletTime;
    public Text brokeTxt2;
    public Text brokeTxt3;
    public Text brokeItem1;
    public Text brokeItem2;
    public Text brokeItem3;
    public Text brokeItem4;
    public Text brokeItem5;
    public TimerForToilet toilet;
    public string timeWhenToiletCleanedPref;
    public int floor;

    private void Awake()
    {
        toilet = gameObject.GetComponent<TimerForToilet>();
        UpdateMsToiletTime();
    }
    void Start()
    {
        CheckArray(); // Start?
        StartCoroutine(CheckForDestroy());
    }
    public void CheckArray()
    {
        int i = 0;
        for (i = 0; i <= stuffAll1Floor.Length - 1; i++)
        {
            if (stuffAll1Floor[i].isDestroyed == false)
            {
                stuffToDestroy1Floor.Add(stuffAll1Floor[i]);
            }
        }
    }

    public void UpdateMsToiletTime()
    {
        switch (floor)
        {
            case 3:
                msToiletTime = PowerForToilet3.instance.msToiletTime;
                break;
            case 2:
                msToiletTime = PowerForToilet2.instance.msToiletTime;
                break;
            case 1:
                msToiletTime = PowerForToilet.instance.msToiletTime;
                break;
            default:
                msToiletTime = PowerForToilet.instance.msToiletTime;
                break;
        }
    }
    private IEnumerator CheckForDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        long tempTimeToiletCleaned = 0;
        if (PlayerPrefs.HasKey(timeWhenToiletCleanedPref))
        {
            tempTimeToiletCleaned = Convert.ToInt64(PlayerPrefs.GetString(timeWhenToiletCleanedPref), null);
        }
        // var tempTimeToiletCleaned = Convert.ToInt64(PlayerPrefs.GetString(timeWhenToiletCleanedPref), null);
        var addedTimeToilet = DateTime.FromBinary(tempTimeToiletCleaned);
        var currentTime = DateTime.Now;
        var difference = currentTime.Subtract(addedTimeToilet);
        var rawTime = (float)difference.TotalSeconds;
        var secAfterAddToilet = (int)rawTime; //возможно не учитывается время работы туалета
        brokeTxt2.text = $"Start cheking secAfterAddToilet ==== {secAfterAddToilet} ";
        brokeTxt3.text = $"TimeWhenToiletCleaned before check == {addedTimeToilet} ";
        // Debug.Log("++++++++++++++++++++++++++++++++++++ secondsToiletLeft > secAfterExit " + secondsToiletLeft + " > " + secAfterExit);
        if (secAfterAddToilet > 30f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy1" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem1.text = $"I've broke item1 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy1" + floor, 1);
            }
        }
        if (secAfterAddToilet > 60f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy2" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem2.text = $"I've broke item2 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy2" + floor, 1);
            }
        }
        if (secAfterAddToilet > 90f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy3" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem3.text = $"I've broke item3 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy3" + floor, 1);
            }
        }
        if (secAfterAddToilet > 120f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy4" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem4.text = $"I've broke item4 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy4" + floor, 1);
            }
        }
        if (secAfterAddToilet > 150f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy5" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem5.text = $"I've broke item5 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy5" + floor, 1);
            }
        }
        if (secAfterAddToilet > 180f + msToiletTime && PlayerPrefs.GetInt("stuffToDestroy6" + floor) != 1)
        {
            if (stuffToDestroy1Floor.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, stuffToDestroy1Floor.Count);
                var element = stuffToDestroy1Floor[i];
                element.BrakeObject();

                foreach (var x in stuffToDestroy1Floor)
                {
                    Debug.Log(x.ToString());
                }
                Debug.Log("------------------------------------------------------------------------------" + stuffToDestroy1Floor.Count);
                stuffToDestroy1Floor.RemoveAt(i);
                brokeItem5.text = $"I've broke item6 {element.name}";
                PlayerPrefs.SetInt("stuffToDestroy6" + floor, 1);
            }
        }
    }
    public void Save()
    {
        CheckArray();
        PlayerPrefs.SetInt("SecondsLeftToiletForDestroyStuff" + floor, toilet.toiletTimer);
        Debug.Log("if (stuffToDestroy1Floor.Count < 1)---------------------------------------------------" + stuffToDestroy1Floor.Count);
        if (stuffToDestroy1Floor.Count < 1)
        {
            PlayerPrefs.SetInt("AllBrokeYouCantBuyCat" + floor, 1);
            Debug.Log("AllBrokeYouCantBuyCat-------------------------save 1----------------------------------------------------- " + floor);
        }
        else
        {
            PlayerPrefs.SetInt("AllBrokeYouCantBuyCat" + floor, 0);
            Debug.Log("AllBrokeYouCantBuyCat-------------------------save 0----------------------------------------------------- " + floor);
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnApplicationFocus(bool focusStatus)
    {

    }
    private void OnDisable()
    {
        Save();
    }
}
