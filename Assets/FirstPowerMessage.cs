using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPowerMessage : MonoBehaviour
{
    
private GameObject message;
    
    
    void Start()
    {
        message = gameObject.transform.GetChild(0).gameObject;
       

        if ( PlayerPrefs.GetInt("FirstPowerMessage") == 0 && PlayerPrefs.GetInt("TotalScore") >= 50 && PlayerPrefs.GetInt("pillow") == 0 ){
            StartCoroutine (FirstTipByPillow());
            PlayerPrefs.SetInt("FirstPowerMessage", 1);
        }


        
    }
        IEnumerator FirstTipByPillow()
    {   
        yield return new WaitForSecondsRealtime(1.5f);
        message.SetActive(true); 
    }
        
    }

  

