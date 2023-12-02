using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarQuestNightCity : MonoBehaviour
{
    [SerializeField] private NightCityLogic nightCityLogic;
    //[SerializeField] private GameObject scoreAnimation;
    [SerializeField] private GameObject carLights;
    [SerializeField] private AudioSource carAudioSource;
    [SerializeField] private AudioClip carSetAlarm;
    [SerializeField] private float pointsToSlider;
    private bool used;

    public void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.CompareTag("Ball") && !used)
        {
            other.GetComponentInChildren<BallNightCityBehaviour>().MakeBallPuff();
            Debug.Log("------------------------------------");
            nightCityLogic.UpdateSlider(pointsToSlider);
            //scoreAnimation.SetActive(true);
            used = true;
            StartCoroutine("StartAlarm");
        }
    }

    private IEnumerator StartAlarm()
    {
        var lightCoroutine = StartCoroutine("StartLights");
        carAudioSource.Play();
        yield return new WaitForSeconds(8);
        carAudioSource.Stop();
        carAudioSource.PlayOneShot(carSetAlarm);
        used = false;
        StopCoroutine(lightCoroutine);
        carLights.SetActive(false);
    }

    private IEnumerator StartLights()
    {
        while (true)
        {
            carLights.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            carLights.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
