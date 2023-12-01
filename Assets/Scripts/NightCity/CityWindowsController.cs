using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityWindowsController : MonoBehaviour 
{
    [SerializeField] private List<WindowCityNight> windows;
    private float nextThrowTime;

    [SerializeField] private DoorCityNight door;

    [Header("Throwing Frequency")]
    [SerializeField] private float minThrowTime;
    [SerializeField] private float maxThrowTime;
    [SerializeField] private float maxThrowTimeLevel1, minThrowTimeLevel1;
    [SerializeField] private float maxThrowTimeLevel2, minThrowTimeLevel2;
    [SerializeField] private float maxThrowTimeLevel3, minThrowTimeLevel3;
    [SerializeField] private float maxThrowTimeLevel4, minThrowTimeLevel4;

    private bool isPeopleMove;
    private int countToMove = 0;
    private void Awake()
    {
        maxThrowTime = 180f;
        minThrowTime = 180f;
        nextThrowTime = Time.time + Random.Range(minThrowTime, maxThrowTime);
    }

    public void UpdateThrowingFrequency(int sliderValue)
    {
        if (sliderValue < 10f)
        {
            maxThrowTime = 180f;
            minThrowTime = 180f;
        }
        if (sliderValue >= 10f && sliderValue < 20)
        {
            maxThrowTime = maxThrowTimeLevel1;
            minThrowTime = minThrowTimeLevel1;
        }
        else if (sliderValue <= 40f && sliderValue > 20)
        {
            maxThrowTime = maxThrowTimeLevel2;
            minThrowTime = minThrowTimeLevel2;
            isPeopleMove = true;
        }
        else if (sliderValue <= 80f && sliderValue > 40)
        {
            maxThrowTime = maxThrowTimeLevel3;
            minThrowTime = minThrowTimeLevel3;
            isPeopleMove = true;
        }
        else if (sliderValue <= 100f && sliderValue > 80)
        {
            maxThrowTime = maxThrowTimeLevel4;
            minThrowTime = minThrowTimeLevel4;
            isPeopleMove = true;
        }
        nextThrowTime = Time.time + Random.Range(minThrowTime, maxThrowTime);
    }

    public void ThrowObjectFromRandomWindow() 
    {
        if (windows.Count == 0)
        {
            return;
        }
        countToMove++;

        if (isPeopleMove && countToMove > 5)
        {
            door.MovePeople();
            isPeopleMove = false;
            countToMove = 0;
        }

        Debug.Log("!!!!!!!!---------------- countToMove " + countToMove);
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
