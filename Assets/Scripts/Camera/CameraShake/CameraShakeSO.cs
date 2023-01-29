using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CameraShakeSO : ScriptableObject
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    public float Duration { get => _duration; }
    public float Strength { get => _strength; }
    public int Vibrato { get => _vibrato; }

}
