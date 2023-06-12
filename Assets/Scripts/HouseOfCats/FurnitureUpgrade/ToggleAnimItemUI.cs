using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimItemUI : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject _gameObjectToToggle;
    [SerializeField] private HouseCatUpgradeUnit _houseCatUpgradeUnit;

    public void Click()
    {
        if (_gameObjectToToggle.active)
        {
            _gameObjectToToggle.SetActive(false);
        }
        else
        {
            if (!_houseCatUpgradeUnit.isActive)
            {
                _houseCatUpgradeUnit.EnableFurnitureUpgradeUI();
            }
            else
            {
                _gameObjectToToggle.SetActive(true);
            }
                                      
        }

    }
}
