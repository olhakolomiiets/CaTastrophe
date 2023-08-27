using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardsLeftGridToggle : MonoBehaviour
{
    [SerializeField] private GameObject _leftGrid;

    private void OnEnable()
    {
        _leftGrid = GameObject.FindWithTag("LeftAwardsGrid");
        _leftGrid.SetActive(false);
    }
    private void OnDisable()
    {
        _leftGrid.SetActive(true);
    }
}
