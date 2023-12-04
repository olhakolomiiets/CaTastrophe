using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadHitCounter : MonoBehaviour
{
    [SerializeField] private BasketBallHeadLogic basketBallLogic;
    [SerializeField] private GameObject _scoreAnim;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !basketBallLogic.ballScored)
        {
            _scoreAnim.SetActive(true);
            basketBallLogic.UpdateBallsAmount(10);
        }
    }
}
