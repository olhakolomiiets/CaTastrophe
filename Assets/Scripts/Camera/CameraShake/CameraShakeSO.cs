using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CameraShakeSO : ScriptableObject
{
    [Header("Level 1")]
    [SerializeField] private float _duration1;
    [SerializeField] private float _strength1;
    [SerializeField] private int _vibrato1;
    [SerializeField] private float _randomness1;

    [Header("Level 2")]
    [SerializeField] private float _duration2;
    [SerializeField] private float _strength2;
    [SerializeField] private int _vibrato2;
    [SerializeField] private float _randomness2;

    [Header("Level 3")]
    [SerializeField] private float _duration3;
    [SerializeField] private float _strength3;
    [SerializeField] private int _vibrato3;
    [SerializeField] private float _randomness3;

    public float Duration1 { get => _duration1; }
    public float Strength1 { get => _strength1; }
    public int Vibrato1 { get => _vibrato1; }
    public float Randomness1 { get => _randomness1; }

    public float Duration2 { get => _duration2; }
    public float Strength2 { get => _strength2; }
    public int Vibrato2 { get => _vibrato2; }
    public float Randomness2 { get => _randomness2; }

    public float Duration3 { get => _duration3; }
    public float Strength3 { get => _strength3; }
    public int Vibrato3 { get => _vibrato3; }
    public float Randomness3 { get => _randomness3; }

}
