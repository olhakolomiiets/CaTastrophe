using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCounter : MonoBehaviour
{
    [SerializeField] private BasketBallLogic basketBallLogic;   
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !basketBallLogic.ballScored)
        {
            Debug.Log("OnTriggerEnter2D__________________  ");
            basketBallLogic.UpdateBallsAmount();
        }
    }
}
