using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStoper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.transform.position.x < this.transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else this.transform.rotation = Quaternion.Euler(0, 180, 0);

            this.GetComponent<Animator>().SetTrigger("Kick");

            this.GetComponent<BoxCollider2D>().isTrigger = true;

            return;
        }
    }
}
