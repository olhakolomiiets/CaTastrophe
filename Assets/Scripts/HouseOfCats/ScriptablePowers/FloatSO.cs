using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    [SerializeField]
    private float value;
    [SerializeField]
    public float maxValue = 10;
    public float Value { get => value; }
    public Sprite icon;
    public string nameCat;

    public event Action <float> ValueChangeBy = delegate {};
    public event Action<float> ValueSetToNewAmount = delegate { };

    public void ChangeAmountBy(float changeBy)
    {
        if (value >= maxValue)
            value = maxValue;
        value += changeBy;
        value = Mathf.Clamp(value, 0f, value);
        ValueChangeBy?.Invoke(changeBy);
    }
    public void SetNewAmount(float newAmount)
    {
        value = newAmount;
        value = Mathf.Clamp(value, 0f, value);
        ValueSetToNewAmount(newAmount);
    }
}

