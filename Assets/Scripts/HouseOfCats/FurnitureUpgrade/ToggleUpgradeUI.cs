using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUpgradeUI : MonoBehaviour, IClickable
{
    [SerializeField] private HouseCatUpgradeUnit _houseCatUpgradeUnit;
    public void Click()
    {

        _houseCatUpgradeUnit.EnableFurnitureUpgradeUI();
    }
}
