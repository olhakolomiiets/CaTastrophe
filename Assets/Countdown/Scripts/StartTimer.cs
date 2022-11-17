using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    private GameObject canvas;
    private GameObject countdown;
    private GameObject countdownText;
    private GameObject countdownImage;
    private RadialCountdown radialCountdown;
    private TextCountdown textCountdown;
    public int idOfCountDown = 19; 
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        countdown = canvas.transform.GetChild(idOfCountDown).gameObject;
        countdownImage = countdown.transform.GetChild(0).gameObject;
        countdownText = countdownImage.transform.GetChild(0).gameObject;
        radialCountdown = countdownImage.GetComponent<RadialCountdown>();
        textCountdown = countdownText.GetComponent<TextCountdown>();
    }
    public void SetCountDown (int seconds) {
    countdown.SetActive(true);    
    radialCountdown.StartCountdown(seconds);
    textCountdown.StartCountdown(seconds);
    }
}