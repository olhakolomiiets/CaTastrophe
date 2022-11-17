using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMessages : MonoBehaviour
{
    
private GameObject message;
private GameObject message2;
private GameObject message3;
private GameObject message4;
private GameObject background;
    
    
    void Start()
    {
        background = gameObject.transform.GetChild(0).gameObject;
        message = gameObject.transform.GetChild(1).gameObject;
        message2 = gameObject.transform.GetChild(2).gameObject;
        message3 = gameObject.transform.GetChild(3).gameObject;
        message4 = gameObject.transform.GetChild(4).gameObject;

        if ( PlayerPrefs.GetInt("FirstMessages") == 0 ){
            background.SetActive(true);
            message.SetActive(true);
            PlayerPrefs.SetInt("FirstMessages", 1);
            PlayerPrefs.SetInt("CatsIsYoursAchieve", 1);
        
        
        
        }


        
    }

  
}
