using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCollaiderChecker : MonoBehaviour
{
    [SerializeField] private BasketBallLogic basketBallLogic;
    public bool topCollaiderEntered;

    private void Start()
    {
        topCollaiderEntered = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            topCollaiderEntered = true;
        }
    }
}
