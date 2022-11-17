using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class Tip : MonoBehaviour
{
    public bool Used = false;
    public GameObject ThisTip;
    public string prefName;
    public string prefNameTip;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Used == false && PlayerPrefs.GetInt(prefName) == 1)
        {
            if (other.CompareTag("ActiveCollaider"))
            {
                if (PlayerPrefs.GetInt(prefNameTip) == 0)
                {
                    ThisTip.SetActive(true);
                }
            }

        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") && PlayerPrefs.GetInt(prefName) == 1)
        {
            Invoke("TipHide", 2);
        }
    }
    private void TipHide()
    {
        ThisTip.SetActive(false);
    }
}