using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MiniGamesSO : ScriptableObject
{
    [SerializeField] private int _star1;
    [SerializeField] private int _star2;
    [SerializeField] private int _star3;

    [SerializeField] private bool isReward1Show;
    [SerializeField] private bool isReward2Show;
    [SerializeField] private bool isReward3Show;    
    
    [SerializeField] private bool isCatHappy;

    public int Star1 { get => _star1; }
    public int Star2 { get => _star2; }
    public int Star3 { get => _star3; }

    public bool IsReward1Show { get => isReward1Show; }
    public bool IsReward2Show { get => isReward2Show; }
    public bool IsReward3Show { get => isReward3Show; }
    public bool IsCatHappy { get => isCatHappy; }


    public void SetIsReward1Show(bool isRewardShow)
    {
        isReward1Show = isRewardShow;
    }

    public void SetIsReward2Show(bool isRewardShow)
    {
        isReward2Show = isRewardShow;
    }

    public void SetIsReward3Show(bool isRewardShow)
    {
        isReward3Show = isRewardShow;
    }    
    
    public void SetCatMood(bool isCatSad)
    {
        isCatHappy = isCatSad;
    }

}

