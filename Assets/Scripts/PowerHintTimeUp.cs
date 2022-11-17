using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHintTimeUp : MonoBehaviour
{    
    void Start()
    {   
        StartCoroutine(ShowPowerHint());
    }
  IEnumerator ShowPowerHint()
    {
        yield return new WaitForSecondsRealtime(0.02f);
         if (PlayerPrefs.GetInt("AreAvailablePower") == 1)
                {
                    PlayerPrefs.SetInt("AreAvailablePower", 0);
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    yield return new WaitForSeconds(3f);
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                }
            }
    }