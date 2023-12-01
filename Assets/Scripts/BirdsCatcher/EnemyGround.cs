using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public float duration = 3.0f;
    public BirdsCatcherLogic birdsCatcherLogic;
    public Animator enemyGroundAnim;
    public float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float timeElapsed = Time.time - startTime;

        if (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Vector3 startPosition = startTransform.position;
            Vector3 endPosition = endTransform.position;

            // Interpolate the position using Lerp
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
        }
        else
        {
            // Snap the object to the final position
            transform.position = endTransform.position;
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().isTrigger = true;

            return;
        }
    }
}
