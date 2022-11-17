using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{
    public static Rigidbody2D rb;
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") | collision.gameObject.tag.Equals("Destroyed")
               | collision.gameObject.tag.Equals("Undestroyed")
               && collision.relativeVelocity.magnitude > 5)
        {
            SoundManager.snd.PlayBallSounds();
        }
    }
}