using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Floor"))
        {
            Debug.Log("HitGround");
        }
        if (col.gameObject.tag.Equals("Floor") && CowController.rb.velocity.y < -15)
        {
            Debug.Log("death");
        }
    }
}