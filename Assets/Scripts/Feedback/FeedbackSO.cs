using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FeedbackSO : ScriptableObject
{
    [SerializeField] private int _askForFeedback = 0;
    public int AskForFeedback { get => _askForFeedback; }

    public void ChangeValue(int changeBy)
    {
        _askForFeedback = changeBy;
    }
}
