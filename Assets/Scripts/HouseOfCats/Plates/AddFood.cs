using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFood : MonoBehaviour, IClickable
{

    public TimerForFood foodTimer;
    public FoodIco[] foodIcons;
    private int allFood;
    [SerializeField] private GameObject noFoodTag;


    public void Click()
    {
        allFood = PlayerPrefs.GetInt("TotalFood");

        if (foodTimer.isPlateFull == true && FreeFoodIco() != null)
        {
            if (allFood > 0)
            {
                PlayerPrefs.SetInt("TotalFood", allFood - 1);
                FreeFoodIco().IconOn();
                if (foodTimer.floor == 2)
                {
                    PowerForFood2.instance.iconsFood = PowerForFood2.instance.iconsFood + 1;
                }
                else if (foodTimer.floor == 3)
                {
                    PowerForFood3.instance.iconsFood = PowerForFood3.instance.iconsFood + 1;
                }
                else 
                {
                    PowerForFood.instance.iconsFood = PowerForFood.instance.iconsFood + 1;
                }

                FirebaseAnalytics.LogEvent(name: "add_food");
            }
            else
            {
                StartCoroutine(NoFood());
            }
        }

        if (foodTimer.isPlateFull == false)
        {
            if (allFood > 0)
            {
                PlayerPrefs.SetInt("TotalFood", allFood - 1);
                foodTimer.Click();

                FirebaseAnalytics.LogEvent(name: "add_Food");

            }
            else
            {
                StartCoroutine(NoFood());
            }
        }
    }

    IEnumerator NoFood()
    {
        noFoodTag.SetActive(true);
        yield return new WaitForSeconds(1f);
        noFoodTag.SetActive(false);
    }

    private FoodIco FreeFoodIco()
    {
        for (int i = 0; i < foodIcons.Length; i++)
        {
            if (!foodIcons[i].isActive)
            {
                return foodIcons[i];
            }
        }
        return null;
    }

}
