using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageStoper : MonoBehaviour
{
    public UnityEvent OnCollisionWithPlayer;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnCollisionWithPlayer.Invoke();

        }
    }
}
