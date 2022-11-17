using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanUp : MonoBehaviour, IClickable
{
    public TimerForToilet toiletTimer;
    public static event PowerForToilet.Toilet1CleanUpDelegate ToiletCleanUp;

    public void Click()
    {
        Debug.Log("PUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUSHHHHHHHHHHHHHHHHH");
        SoundManager.snd.PlayButtonsSound();
        toiletTimer.Clean();
        ToiletCleanUp?.Invoke();
    }
}