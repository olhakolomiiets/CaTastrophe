using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public static Rigidbody2D rb;
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 vel = rb.velocity;
        if (col.gameObject.tag.Equals("Floor"))
        {
            Debug.Log("HitGround222222222");
        }
        if (col.gameObject.tag.Equals("Floor") && vel.magnitude < -10)
        {
            Debug.Log("death222");
        }
    }
}
