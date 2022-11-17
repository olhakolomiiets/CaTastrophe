using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHouse : MonoBehaviour, IClickable
{
    public GameObject plateCanvas;
    public void Click()
    {
        if (plateCanvas.active){
            plateCanvas.SetActive(false);}
        else {
            plateCanvas.SetActive(true); 
            }
    }
}
