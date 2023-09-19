using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MiniGamesSO : ScriptableObject
{
    [SerializeField] private int _star1;
    [SerializeField] private int _star2;
    [SerializeField] private int _star3;
    public int Star1 { get => _star1; }
    public int Star2 { get => _star2; }
    public int Star3 { get => _star3; }
}

