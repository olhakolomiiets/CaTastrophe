using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradePricesSO : ScriptableObject
{
    #region EditorFields

    [SerializeField] private int _upgradePriceToiletLvl2;
    [SerializeField] private int _upgradePriceToiletLvl3;
    [SerializeField] private int _upgradePriceFoodPlateLvl2;
    [SerializeField] private int _upgradePriceFoodPlateLvl3;

    #endregion

    #region Properties
    public int UpgradePriceToiletLvl2 => _upgradePriceToiletLvl2;
    public int UpgradePriceToiletLvl3 => _upgradePriceToiletLvl3;
    public int UpgradePriceFoodPlateLvl2 => _upgradePriceFoodPlateLvl2;
    public int UpgradePriceFoodPlateLvl3 => _upgradePriceFoodPlateLvl3;
    #endregion
}
