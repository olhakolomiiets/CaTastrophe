using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AchieveSO : ScriptableObject
{
    #region EditorFields
    [SerializeField] private string _achieveNamePref;

    [Header("Achievements")]
    [SerializeField] private int _targetNumberLvl1;
    [SerializeField] private int _targetNumberLvl2;
    [SerializeField] private int _targetNumberLvl3;

    [SerializeField] private GameObject _achieveLvl1;
    [SerializeField] private GameObject _achieveLvl2;
    [SerializeField] private GameObject _achieveLvl3;

    [Header("Houses")]
    [SerializeField] private int _targetNumber;
    [SerializeField] private int _needStars;
    [SerializeField] private GameObject _houseAchieve;
    #endregion

    #region Properties
    public string AchieveNamePref => _achieveNamePref;
    public int TargetNumberLvl1 => _targetNumberLvl1;
    public int TargetNumberLvl2 => _targetNumberLvl2;
    public int TargetNumberLvl3 => _targetNumberLvl3;
    public GameObject AchieveLvl1 => _achieveLvl1;
    public GameObject AchieveLvl2 => _achieveLvl2;
    public GameObject AchieveLvl3 => _achieveLvl3;

    public int TargetNumber => _targetNumber;
    public int NeedStars => _needStars;
    public GameObject HouseAchieve => _houseAchieve;
    #endregion

    [Header("Other")]
    [SerializeField] private int _realNumber;
    [SerializeField] private int _numberOfStars;

    private void Awake()
    {
        _realNumber = PlayerPrefs.GetInt(AchieveNamePref);
    }

    public int GetAchieveScore()
    {
        if (PlayerPrefs.HasKey(AchieveNamePref))
        {
            _realNumber = PlayerPrefs.GetInt(AchieveNamePref);
            return PlayerPrefs.GetInt(AchieveNamePref);
        }
        else
        {
            return 0;
        }
    }

    public int GetAllStars()
    {
        if (PlayerPrefs.HasKey("AllStars"))
        {
            _numberOfStars = PlayerPrefs.GetInt("AllStars");
            return PlayerPrefs.GetInt("AllStars");
        }
        else
        {
            return 0;
        }
    }
}