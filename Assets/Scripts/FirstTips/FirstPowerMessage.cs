using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPowerMessage : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstPowerMessage") == 0 && PlayerPrefs.GetInt("TotalScore") >= 50 && PlayerPrefs.GetInt("pillow") == 0)
        {
            PersistentEventManager.Instance.TriggerEvent("FirstPowerMessage");
            PlayerPrefs.SetInt("FirstPowerMessage", 1);
        }
    }
}