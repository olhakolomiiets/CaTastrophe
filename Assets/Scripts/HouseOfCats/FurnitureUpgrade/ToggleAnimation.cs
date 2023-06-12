using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour, IClickable
{
    [SerializeField] private HouseCatUpgradeUnit _houseCatUpgradeUnit;
    public void Click()
    {
        _houseCatUpgradeUnit.ToggleAnimationOfActiveObject();

    }
}
