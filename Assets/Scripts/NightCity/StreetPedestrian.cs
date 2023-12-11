using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetPedestrian : MonoBehaviour
{
    public GameObject human;
    public Transform startTransform;
    public Transform endTransform;
    public float duration = 3.0f;
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
            transform.position = endTransform.position;
            human.gameObject.SetActive(false);
            StartCoroutine("RestartPedestian");
        }
    }

    private IEnumerator RestartPedestian()
    {
        transform.position = startTransform.position;
        yield return new WaitForSeconds(2f);
        startTime = Time.time;
        human.gameObject.SetActive(true);
    }
}
