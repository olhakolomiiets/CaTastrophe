using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPowerRestore : MonoBehaviour
{
    [SerializeField] private GameObject firstPowerRestore;
    [SerializeField] private List<FloatSO> catPowersSO;

    void Start()
    {
        foreach (FloatSO power in catPowersSO)
        {
            if (PlayerPrefs.HasKey("firstPowerRestore") == false && power.Value < 3f)
            {
                firstPowerRestore.SetActive(true);
                PlayerPrefs.SetInt("firstPowerRestore", 1);
            }
        }
    }

    public void RestoreAllPowersForTheFirstTime()
    {
        foreach (FloatSO power in catPowersSO)
        {
            if (PlayerPrefs.GetInt("firstPowerRestore") == 1 && power.Value < 3f)
            {
                PlayerPrefs.SetInt("firstPowerRestore", 0);
                firstPowerRestore.SetActive(false);
                power.SetNewAmount(10f);
            }
        }
    }
}
