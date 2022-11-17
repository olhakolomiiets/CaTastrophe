using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DestructableObjectHandler : MonoBehaviour
{
    public GameObject destroyedVersion;
    private GameObject fixedVersion;
    public GameObject dustForUpgrade;
    private GameObject dust;
    public bool isDestroyed;
    public string prefsNameForDestrObject;
    private float msToiletTime = 60f;
    [SerializeField]
    public GameObject ico;
    public Color icoColor;
    public bool softRepairSound;
    public int floor;
    [SerializeField] private int repairCost;
    [SerializeField] private GameObject priceObject;
    private int price;
    [SerializeField] private Text priceText;
    private int TotalScore;
    [SerializeField] private GameObject noMoneyTag;

    private void Awake()
    {
        UpdateMsToiletTime();
        fixedVersion = gameObject.transform.GetChild(0).gameObject;
        destroyedVersion = gameObject.transform.GetChild(1).gameObject;
        if (PlayerPrefs.GetInt(prefsNameForDestrObject) == 1)
        {
            fixedVersion.SetActive(false);
            destroyedVersion.SetActive(true);
            ico.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            isDestroyed = true;
        }
        else
        {
            fixedVersion.SetActive(true);
            destroyedVersion.SetActive(false);
            isDestroyed = false;
            ico.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
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
    public void BrakeObject()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            PlayerPrefs.SetInt(prefsNameForDestrObject, 1);
            fixedVersion.SetActive(false);
            destroyedVersion.SetActive(true);
        }
    }
    public void RepairObject()
    {
        UpdateMsToiletTime();
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        if (isDestroyed == true)
        {
            if (TotalScore >= repairCost)
            {
                if (softRepairSound)
                {
                    SoundManager.snd.PlayRepairSoft();
                }
                else SoundManager.snd.PlayRepairHard();

                dust = Instantiate(dustForUpgrade, destroyedVersion.transform.position, Quaternion.identity);
                PlayerPrefs.SetInt(prefsNameForDestrObject, 0);
                fixedVersion.SetActive(true);
                destroyedVersion.SetActive(false);
                isDestroyed = false;
                ResetStuffToDestroy();
                price = TotalScore - repairCost;
                priceText.text = repairCost.ToString();
                priceObject.SetActive(true);
                ico.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .5f);
                var currentTime = DateTime.Now;
                var theTime = currentTime.AddSeconds(-msToiletTime);
                // PlayerPrefs.SetString("timeWhenToiletCleanedPref" + floor, theTime.ToBinary().ToString());
                PlayerPrefs.SetInt("TotalScore", price);
                PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref" + floor, theTime.ToBinary().ToString());
            }
            else if (TotalScore < repairCost)
            {
                StartCoroutine(NoMoney());
            }
        }
    }

    public void Click()
    {
        RepairObject();
    }
    public void ResetStuffToDestroy()
    {
        PlayerPrefs.SetInt("stuffToDestroy2" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy1" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy3" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy4" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy5" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy6" + floor, 0);
    }

    IEnumerator NoMoney()
    {

        noMoneyTag.SetActive(true);

        yield return new WaitForSeconds(1f);

        noMoneyTag.SetActive(false);
    }

}
