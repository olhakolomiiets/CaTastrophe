using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesQueueAll : MonoBehaviour
{
    public FoodIco[] foodIcons;
    public float pointsWhenYouWereAbsent;
    // public FoodTimer foodTimer;
    public int arraySize;
    void OnApplicationQuit()
    {
        pointsWhenYouWereAbsent = 0f;
    }
    
    public int HowManyActiveFood()
    {
        for (int i = arraySize; i >= 0; i--)
        {
            if (i == 2)
            {
                if (!foodIcons[2].foodButton.IsInteractable())
                {
                    return 3;
                }
            }
            if (i == 1)
            {
                if (!foodIcons[1].foodButton.IsInteractable())
                {
                    return 2;
                }
            }
            else if (!foodIcons[0].foodButton.IsInteractable())
            {
                return 1;
            }
        }
        return 0;
    }
    public bool IsActiveIcons()
    {

        for (int i = arraySize; i >= 0; i--)
        {
            if (!foodIcons[i].foodButton.IsInteractable())
            {
                foodIcons[i].foodButton.interactable = true;
                foodIcons[i].foodButtonYes.SetActive(false);
                return true;
            }
        }
        return false;
    }
}
