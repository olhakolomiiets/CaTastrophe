using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlreadyBoughtCatShop : MonoBehaviour
{
    public string ppNamePower;
    private int purch;
    [SerializeField] private GameObject doneIco;
    bool isDone = false;
    void Start()
    {
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
        if (doneIco != null)
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
}