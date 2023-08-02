using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFloor : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject buttonFloorToOpen;
    [SerializeField] private GameObject buttonFloorToClose; 

    public void Click()
    {
        CloseAllBuyFloorWindows();
        buttonFloorToOpen.SetActive(true);
    }

    public void CloseAllBuyFloorWindows()
    {
        buttonFloorToClose.SetActive(false);
    }

}
