using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreAvailablePower : MonoBehaviour
{
    public Component[] buyPowers;
    [SerializeField] private GameObject NotificationImg;
    

    void Start()
    {
        Debug.Log("Start AreAvailablePower" + PlayerPrefs.GetInt("AreAvailablePower"));
        buyPowers = GetComponentsInChildren<buyPowers>();   
        CheckPowers();           
    }
      public int GetTotalScore()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            return PlayerPrefs.GetInt("TotalScore");
        }
        else
        {
            return 0;
        }
    }
    public void CheckPowers()
    {
        if (NotificationImg != null)
        {
            NotificationImg.SetActive(false);
        }

        foreach (buyPowers power in buyPowers)
          
            if (power.isPurchased == false)
            {
                if (power.price <= GetTotalScore())
                {
                    PlayerPrefs.SetInt("AreAvailablePower", 1);
                    if (NotificationImg != null)
                    {
                        NotificationImg.SetActive(true);
                    }
                }
            }           
    }
   
}