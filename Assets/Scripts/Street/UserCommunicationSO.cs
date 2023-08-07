using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UserCommunicationSO : ScriptableObject
{
    [SerializeField] private int _askForFeedback = 0;
    [SerializeField] private int _offerExtraLife = 0;
    public int AskForFeedback { get => _askForFeedback; }
    public int OfferExtraLife { get => _offerExtraLife; }

    public void ChangeValue(int changeBy)
    {
        _askForFeedback = changeBy;
    }

    public void ChangeValueExtraLifeSpecial(int changeBy)
    {
        _offerExtraLife += changeBy;
    }
}
