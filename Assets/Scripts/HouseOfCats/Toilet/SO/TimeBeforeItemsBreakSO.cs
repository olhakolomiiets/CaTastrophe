using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class TimeBeforeItemsBreakSO : ScriptableObject
{
    #region EditorFields

    [SerializeField] private float _timeBeforeItem1Break;
    [SerializeField] private float _timeBeforeItem2Break;
    [SerializeField] private float _timeBeforeItem3Break;
    [SerializeField] private float _timeBeforeItem4Break;
    [SerializeField] private float _timeBeforeItem5Break;
    [SerializeField] private float _timeBeforeItem6Break;
    #endregion

    #region Properties
    public float Item1 => _timeBeforeItem1Break;
    public float Item2 => _timeBeforeItem2Break;
    public float Item3 => _timeBeforeItem3Break;    
    public float Item4 => _timeBeforeItem4Break;
    public float Item5 => _timeBeforeItem5Break;
    public float Item6 => _timeBeforeItem6Break;
    #endregion
}
