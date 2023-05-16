using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerForFood : MonoBehaviour, IClickable
{
    public FoodIco[] foodIcons;
    public GameObject foodButtonYes;
    public GameObject foodInPlateImg;
    public Text uiTextTimer;
    public float foodTimer;
    public bool isPlateFull;
    public int floor;
    bool checkStarts = false;
    [SerializeField] public IPlateInterface platePowerComponent;
    public static event PowerForFood.Plate1FoodAddDelegate PlateAddFood;
    public static event PowerForFood2.Plate2FoodAddDelegate Plate2AddFood;
    public static event PowerForFood3.Plate3FoodAddDelegate Plate3AddFood;
    bool powerConnected;

    private void Awake()
    {
        Application.runInBackground = false;
        ConnectPowerForFood();
    }

    void Start()
    {
        ConnectPowerForFood();
        if (platePowerComponent.IconsFood() == 1)
        {
            foodIcons[0].IconOn();
        }
        if (platePowerComponent.IconsFood() == 2)
        {
            foodIcons[0].IconOn();
            foodIcons[1].IconOn();
        }
        if (platePowerComponent.IconsFood() == 3)
        {
            foodIcons[0].IconOn();
            foodIcons[1].IconOn();
            foodIcons[2].IconOn();
        }

    }

    void Update()
    {
            if (platePowerComponent.FoodTimer() >= platePowerComponent.GetMsFoodTime())
            {
            if (isPlateFull)
            {
                FoodIconsOff();
            }
            }
            else
            {
            var countdown = platePowerComponent.GetMsFoodTime() - platePowerComponent.FoodTimer();

            float minutes = Mathf.FloorToInt(countdown / 60);
            float seconds = Mathf.FloorToInt(countdown % 60);
            uiTextTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (!isPlateFull)
            {
                foodButtonYes.SetActive(true);
                foodInPlateImg.SetActive(true);
                isPlateFull = true;
            }
        }
    }

    public void FoodIconsOff()
    {
        if (platePowerComponent.IconsFood() == 0)
        {
            foodButtonYes.SetActive(false);
            foodInPlateImg.SetActive(false);
            isPlateFull = false;
        }
        else
        {
            for (int i = foodIcons.Length - 1; i >= 0; i--)
            {
                if (platePowerComponent.IconsFood() == i + 1)
                {
                    foodIcons[i].IconOff();
                }
            }
        }
    }
    public bool IsFoodEnd()
    {
        if (!checkStarts)
        {
            checkStarts = true;
        }
        if (platePowerComponent.FoodTimer() >= platePowerComponent.GetMsFoodTime())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Click()
    {
        foodButtonYes.SetActive(true);
        SendAddFoodEvent();
    }

    private void SendAddFoodEvent()
    {
        if (floor == 2)
        {
            Plate2AddFood?.Invoke();
        }
        else if (floor == 3)
        {
            Plate3AddFood?.Invoke();
        }
        else PlateAddFood?.Invoke();
    }

    public void ConnectPowerForFood()
    {
        if (floor == 2)
        {
            platePowerComponent = GameObject.FindObjectOfType<PowerForFood2>(); 
        }
        else if (floor == 3)
        {
            platePowerComponent = GameObject.FindObjectOfType<PowerForFood3>();
        }
        else
        {
            platePowerComponent = GameObject.FindObjectOfType<PowerForFood>();
        }
    }

}
