using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFloor : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject buttonFloor1;

    public void Click()
    {
        buttonFloor1.SetActive(true);
    }

}
