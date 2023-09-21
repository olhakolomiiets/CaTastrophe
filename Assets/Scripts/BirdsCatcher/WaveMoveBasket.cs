using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMoveBasket : MonoBehaviour
{
    public Transform startTransform;
    public Transform endTransform;
    public float duration = 2.0f;
    public float waveAmplitude = 1.0f;

    private float startTime;
    private float timeElapsed;

    [SerializeField] private OnTriggerStartAnimLogic onTriggerLogic;

    public float minBirdTimeWaiting = 10f;
    public float maxBirdTimeWaiting = 20f;

    private bool isMoving = true; // Start as false

    void Start()
    {
        startTime = Time.time;
        onTriggerLogic.triggerEntered.AddListener(OnTriggerEnteredLogic); 
    }

    void Update()
    {
        if (onTriggerLogic.coroutineIsRunning)
        {
            return;
        }
        if (isMoving)
        {
            timeElapsed = Time.time - startTime;

            if (timeElapsed < duration)
            {
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
                StartCoroutine(UpdateStartTime());
            }
        }
    }

    private void OnTriggerEnteredLogic()
    {
        startTime = startTime + 1f;
        MakeFeatherPuff();
        SoundManager.snd.PlayBirdHitSounds();
    }

    private void MakeFeatherPuff()
    {
        GameObject featherPuff = ObjectPooler.SharedInstance.GetPooledObject("PoolObject6");
        if (featherPuff != null)
        {
            featherPuff.transform.position = transform.position;
            featherPuff.SetActive(true);
            StartCoroutine(FeatherDeactivator(featherPuff));
        }
    }

    private IEnumerator UpdateStartTime()
    {
        float randomX = UnityEngine.Random.Range(minBirdTimeWaiting, maxBirdTimeWaiting);
        yield return new WaitForSeconds(randomX);
        transform.position = startTransform.position;
        isMoving = true; // Allow movement to start again
        startTime = Time.time;
    }

    private IEnumerator FeatherDeactivator(GameObject featherPuff)
    {
        yield return new WaitForSeconds(4);
        featherPuff.SetActive(false);
    }

    private void OnDisable()
    {
        onTriggerLogic.triggerEntered.RemoveListener(OnTriggerEnteredLogic);
    }
}