using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    [SerializeField] private BasketBallHeadLogic basketBallHeadLogic;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            basketBallHeadLogic.Do();
        }
    }
}
