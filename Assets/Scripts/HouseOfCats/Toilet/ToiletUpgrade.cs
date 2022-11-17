using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletUpgrade : MonoBehaviour, IClickable
{
    public ToiletUpgradeHandler handler;

    public void Click()
    {
        handler.Click();
    }


}
