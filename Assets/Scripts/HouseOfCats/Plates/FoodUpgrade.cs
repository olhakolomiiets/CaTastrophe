using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUpgrade : MonoBehaviour, IClickable
{
    public FoodUpgradeHandler handler;

    public void Click()
    {
        handler.Click();
    }


}
