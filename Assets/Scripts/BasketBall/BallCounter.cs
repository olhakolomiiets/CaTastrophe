using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    [SerializeField] private BasketBallLogic basketBallLogic;
    [SerializeField] private TopCollaiderChecker topCollaider;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!topCollaider.topCollaiderEntered)
        {
            return;
        }
        if (other.CompareTag("Ball") && !basketBallLogic.ballScored)
        {
            basketBallLogic.UpdateBallsAmount();
            topCollaider.topCollaiderEntered = false;
        }
    }
}
