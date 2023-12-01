using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStoper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;

            return;
        }
    }
}
