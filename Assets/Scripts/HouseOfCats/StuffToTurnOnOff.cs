using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffToTurnOnOff : MonoBehaviour, IClickable
{
    public GameObject activeObject;
    public GameObject inactiveObject;
    public void Click()
    {
        if (activeObject.active){
            activeObject.SetActive(false);
            inactiveObject.SetActive(true);
            }
        else {
            activeObject.SetActive(true); 
            inactiveObject.SetActive(false);
            }
    }
}
