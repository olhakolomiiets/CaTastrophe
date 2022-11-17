using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlreadyBoughtShop : MonoBehaviour
{
    public string ppNamePower;
    private int purch;
    private GameObject doneIco;
    bool isDone = false;
    void Start()
    {
        doneIco = this.gameObject.transform.GetChild(2).gameObject;
        IsBought();
    }
    private void Update()
    {
        if (isDone == false)
        {
            IsBought();
        }
    }
    public void IsBought()
    {
        purch = PlayerPrefs.GetInt(ppNamePower, 0);
        if (purch == 0)
        {
            doneIco.SetActive(false);
        }
        else
        {
            doneIco.SetActive(true);
            isDone = true;
        }
    }
}