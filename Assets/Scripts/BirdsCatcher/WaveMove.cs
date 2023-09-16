using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMove : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public float duration = 2.0f;
    public float waveAmplitude = 1.0f;
    public float startTime;
    private float timeElapsed;

    public BirdToCatch birdToCatch;
    public float minBirdTimeWaiting = 10f;
    public float maxBirdTimeWaiting = 20f;

    public bool isMoving = true; // Start as false
    public float poopTime;
    private bool alreadyPooped;
    void Start()
    {
        //isMoving = false;
        startTime = Time.time;      
    }

    void Update()
    {
        if (birdToCatch.coroutineIsRunning)
        {
            return;
        }
        if (isMoving)
        {
            timeElapsed = Time.time - startTime;

            if (timeElapsed < duration)
            {
                if (timeElapsed >= poopTime && !alreadyPooped)
                {
                    if(poopTime == 0)
                    {
                        alreadyPooped = true;
                        return;
                    }
                    var poop = ObjectPooler.SharedInstance.GetPooledObject("Poop");
                    poop.gameObject.transform.position = this.transform.position;
                    poop.SetActive(true);
                    alreadyPooped = true;
                    
                }
                    float t = timeElapsed / duration;
                Vector3 startPosition = startTransform.position;
                Vector3 endPosition = endTransform.position;

                // Calculate a clamped wave motion using Mathf.Sin
                float waveOffset = Mathf.Sin(t * Mathf.PI * 2.0f) * waveAmplitude;
                waveOffset = Mathf.Clamp(waveOffset, -waveAmplitude, waveAmplitude);

                // Interpolate the position using Lerp
                transform.position = Vector3.Lerp(startPosition, endPosition, t) + Vector3.up * waveOffset;
            }
            else
            {
                transform.position = endTransform.position;
                isMoving = false;              
                if (birdToCatch.featherPuff != null)
                {
                    birdToCatch.featherPuff.SetActive(false);
                }
                if (birdToCatch.isEnemy)
                {
                    this.GetComponent<Collider2D>().isTrigger = false; // for enemy birds
                }               
                this.gameObject.SetActive(false);
                alreadyPooped = false;
            }
        }
    }
}