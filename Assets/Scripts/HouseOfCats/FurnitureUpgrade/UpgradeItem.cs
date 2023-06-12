using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    [SerializeField] private GameObject _upgradeItemIcoPrefab;
    public string upgradeUnitPref;
    public GameObject UpgradeItemIcoPrefab => _upgradeItemIcoPrefab;
}
