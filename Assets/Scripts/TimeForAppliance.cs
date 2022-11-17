using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeForAppliance : MonoBehaviour
{
    [SerializeField] private Text text;
    public bool WithSeconds;
    void Start()
    {
        StartCoroutine(Updatetime());
    }
    private IEnumerator Updatetime()
    {
        while (true)
        {
            var today = System.DateTime.Now;
            if (WithSeconds)
            {
                text.text = today.ToString("HH:mm:ss");
            }
            else
            {
                text.text = today.ToString("HH:mm");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}