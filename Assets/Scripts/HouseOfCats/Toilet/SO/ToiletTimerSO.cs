using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ToiletTimerSO : ScriptableObject
{
    #region EditorFields

    [SerializeField] private float _msToiletTimeLvl1;
    [SerializeField] private float _msToiletTimeLvl2;
    [SerializeField] private float _msToiletTimeLvl3;
    #endregion

    #region Properties
    public float MsToiletTimeLvl1 => _msToiletTimeLvl1;
    public float MsToiletTimeLvl2 => _msToiletTimeLvl2;
    public float MsToiletTimeLvl3 => _msToiletTimeLvl3;
    #endregion
}
