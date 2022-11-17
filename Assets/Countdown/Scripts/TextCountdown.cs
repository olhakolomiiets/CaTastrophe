using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextCountdown : MonoBehaviour
{
    [Header("Text")]
    public Text text;
    [SerializeField] private char characterSpliter = ':';

    [Header("Logic")]
    private float lastStart;
    private float duration;
    private bool isActive;

    [Header("End")]
    [SerializeField] private UnityEvent onEnd;

    public void Update()
    {
        if (isActive)
            UpdateText();
    }
    private void UpdateText()
    {
        // Get the amount time left
        float left = (lastStart + duration) - Time.time;

        float seconds = (left % 60);
        float minutes = ((int)(left / 60) % 60);
        float hours = (int)(left / 3600);
        text.text = seconds.ToString("00");

        // Invoke a callback when we're done
        if (left <= 0)
        {
            isActive = false;
            onEnd?.Invoke();
        }
    }

    public void StartCountdown(float seconds)
    {
        lastStart = Time.time;
        isActive = true;
        duration = seconds;
        UpdateText();
    }

    public float GetTimeLeft()
    {
        return (lastStart + duration) - Time.time; ;
    }
}
