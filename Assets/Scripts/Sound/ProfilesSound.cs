﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]

public class Volume {

    public string name;
    public float volume = 1f;
    public float tempVolume =1f;
}

public class Settings 
{
    public static ProfilesSound profile;
}

[CreateAssetMenu(menuName = "Sound/Create Profile")]
public class ProfilesSound : ScriptableObject 
{
    public bool saveInPlayerPrefs = true;
    public string prefPrefix = "Settings_";
     public AudioMixer audioMixer;
     public Volume[] volumeControl;
     

       public void SetProfile(ProfilesSound profile)
    {
        Settings.profile = profile;
    }

    public float GetAudioLevels (string name)
    {
        float volume = 1f;

        if (!audioMixer)
        {
            Debug.LogWarning("No Audiomixer");
            return volume;
        }
        for (int i = 0; i<volumeControl.Length; i++ )
        {
            if (volumeControl[i].name !=name)
            {
                continue;
            }
            else
            {
                if (saveInPlayerPrefs)
                {
                    if(PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }
                volumeControl[i].tempVolume = volumeControl[i].volume;

 if (audioMixer)
        audioMixer.SetFloat(volumeControl[i].name, Mathf.Log (volumeControl[i].volume) *20f);
        volume = volumeControl[i].volume;
                break;
            }
        }
        return volume;
    }



    public void GetAudioLevels ()
    {
        if (!audioMixer)
        {
            Debug.LogWarning("No Audiomixer");
            return;
        }

        for (int i = 0; i < volumeControl.Length; i++)
        {
            if(saveInPlayerPrefs)
            {
                if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                {
                volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                }
            }
        //reset audio vol
        volumeControl[i].tempVolume = volumeControl[i].volume;
        //set mixer to match the volume
    audioMixer.SetFloat(volumeControl[i].name, Mathf.Log (volumeControl[i].volume) *20f);
    }
    }

public void SetAudioLevels (string name, float volume) 
{

if (!audioMixer)
{
    Debug.LogWarning("No Audiomixer");
            return;
}

for (int i = 0; i < volumeControl.Length; i++)
{
     if (volumeControl[i].name !=name)
            {
                continue;
            }
            else
            {
            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log (volume) *20f);
            volumeControl[i].tempVolume = volume;
            break;
    
            }
    }

}

public void SaveAudioLevels () 
{
if (!audioMixer)
{
    Debug.LogWarning("No Audiomixer");
            return;
}

float volume = 0f;
 for (int i = 0; i < volumeControl.Length; i++)
        {
            volume = volumeControl[i].tempVolume;
            if(saveInPlayerPrefs)
            {
            PlayerPrefs.SetFloat(prefPrefix + volumeControl[i].name, volume);
            }
            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log (volume) *20f);
            volumeControl[i].tempVolume = volume;
        }
}
}