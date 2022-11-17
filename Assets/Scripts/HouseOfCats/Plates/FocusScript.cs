using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusScript : MonoBehaviour
{
    // public GameObject savePause;
    // public GameObject loadPause;
    // public GameObject save;
    // public GameObject load;
    public TimerForFood[] foodTimers;
    public TimerForFood foodTimer1;
    public TimerForFood foodTimer2;
    public TimerForFood foodTimer3;
    public TimerForToilet toiletTimer1;
    public TimerForToilet toiletTimer2;
    public TimerForToilet toiletTimer3;
    public int foodTimerActive = 0;
    public int toiletTimerActive = 0;

    private void Awake()
    {
        Application.runInBackground = false;
    }
    public void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // SaveAllLevel();
            // Debug.Log("Save OnPause !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // savePause.SetActive(true);
        }
        else
        {
            // LoadAllLevel();
            // Debug.Log("Load OnPause !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // loadPause.SetActive(true);
        }
    }
    // #if UNITY_ANDROID || UNITY_EDITOR
    // public void OnApplicationFocus(bool hasFocus)
    // {
    //     if (hasFocus)
    //     {
    //         // Debug.Log("Load OnOnFocus Start !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //         LoadAllLevel(); 
    //         // Debug.Log("Load OnOnFocus Finish !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");      
    //         // load.SetActive(true);
    //     }
    //     else
    //     {
    //         // Debug.Log("Save OnOnFocus Start !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //         SaveAllLevel();
    //         // Debug.Log("Save OnOnFocus Finish !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //         // save.SetActive(true);
    //     }
    // }
    // #endif
    // public void SaveAllLevel()
    // {
    //     switch (foodTimerActive)
    //     {
    //         case 1:
    //             // Debug.Log("Save Food 1 Start 1111111111111111111111111111111111111" + gameObject.name);
    //             // foodTimer1.Save();
    //             // Debug.Log("Save Food 1 Finish 111111111111111111111111111111111111");
    //             break;
    //         case 2:
    //             // foodTimer2.Save();
    //             break;
    //         case 3:
    //             // foodTimer3.Save();
    //             break;
    //     }
    //     switch (toiletTimerActive)
    //     {
    //         // case 1:
    //         //     // Debug.Log("Save Toilet Start !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + gameObject.name);
    //         //     toiletTimer1.Save();
    //         //     // Debug.Log("Save Toilet Finish !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //         //     break;
    //         // case 2:
    //         //     toiletTimer2.Save();
    //         //     break;
    //         // case 3:
    //         //     toiletTimer3.Save();
    //         //     break;
    //     }
    // }
    public void LoadAllLevel()
    {
        switch (foodTimerActive)
        {
            case 1:
                // Debug.Log("Load Food 1 Start 111111111111111111111111111111111111" + gameObject.name);
                // foodTimer1.Load();
                // Debug.Log("Load Food 1 Finish 111111111111111111111111111111111111");
                break;
            case 2:
                // foodTimer2.Load();
                break;
            case 3:
                // foodTimer3.Load();
                break;
        }
        switch (toiletTimerActive)
        {
            case 1:
                // Debug.Log("Load Toilet 1 Start !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + gameObject.name);
                // toiletTimer1.Load();
                // Debug.Log("Load Toilet 1 Finish !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;
            case 2:
                // toiletTimer2.Load();
                break;
            case 3:
                // toiletTimer3.Load();
                break;
        }
    }
}
