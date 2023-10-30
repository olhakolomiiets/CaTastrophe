using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityWindowsController : MonoBehaviour 
{
    [SerializeField] private List<WindowCityNight> windows;
    private float nextThrowTime;

    [Header("Throwing Frequency")]
    [SerializeField] private float minThrowTime;
    [SerializeField] private float maxThrowTime;
    [SerializeField] private float throwTimeLevel1;
    [SerializeField] private float throwTimeLevel2;
    [SerializeField] private float throwTimeLevel3;
    [SerializeField] private float throwTimeLevel4;


    public void UpdateThrowingFrequency(int sliderValue)
    {
        if (sliderValue <= 20f && sliderValue !> 20)
        {
            maxThrowTime = throwTimeLevel1;
        }
        else if (sliderValue <= 40f && sliderValue > 20)
        {
            maxThrowTime = throwTimeLevel2;
        }
        else if (sliderValue <= 70f && sliderValue > 40)
        {
            maxThrowTime = throwTimeLevel3;
        }
        else if (sliderValue <= 100f && sliderValue > 70)
        {
            maxThrowTime = throwTimeLevel4;
        }
        //maxThrowTime = nextThrowTime;
    }

    public void ThrowObjectFromRandomWindow() 
    {
        if (windows.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, windows.Count);
        if (!windows[randomIndex].isMoving)
        {
            windows[randomIndex].ThrowObject();
        }
        else
        {
            ThrowObjectFromRandomWindow();
        }
        
    }

    void Update()
    {
        if (Time.time >= nextThrowTime)
        {
            ThrowObjectFromRandomWindow();
            // Calculate the next spawn time
            nextThrowTime = Time.time + Random.Range(minThrowTime, maxThrowTime);
        }
    }
}
