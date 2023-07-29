using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMessages : MonoBehaviour
{

    [SerializeField] private GameObject message;
    [SerializeField] private GameObject message2;
    [SerializeField] private GameObject message3;
    [SerializeField] private GameObject message4;
    [SerializeField] private GameObject message5;
    [SerializeField] private GameObject background;


    void Start()
    {
        if (PlayerPrefs.GetInt("FirstMessages") == 0)
        {
            background.SetActive(true);
            message.SetActive(true);
            PlayerPrefs.SetInt("FirstMessages", 1);
            PlayerPrefs.SetInt("CatsIsYoursAchieve", 1);
        }
    }
}
