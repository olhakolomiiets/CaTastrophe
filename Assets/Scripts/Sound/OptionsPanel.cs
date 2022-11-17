using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    private int music;
     Toggle m_Toggle;
    public AudioMixerGroup Mixer;
    // Start is called before the first frame update
    void Start()
    {
        m_Toggle = gameObject.transform.GetChild(2).GetComponent<Toggle>();
        if (PlayerPrefs.HasKey("music")){
        m_Toggle.isOn = PlayerPrefs.GetInt("music") == 1 ? true : false;;
        }
      // int currentToggleState = m_Toggle.isOn == true ? 1 : 0;
     //  PlayerPrefs.SetInt("music", currentToggleState);
       
    }

    // Update is called once per frame
    void Update()
    {
         music = PlayerPrefs.GetInt("music");
    }
    public void ToggleMusic (bool enabled)
    {

if (enabled)
Mixer.audioMixer.SetFloat("MusicBool", 0);
PlayerPrefs.SetInt("music", 1);
if (!enabled)
Mixer.audioMixer.SetFloat("MusicBool", -80);
PlayerPrefs.SetInt("music", 0);
    }
    public void ChangeVolume (float volume)
    {
Mixer.audioMixer.SetFloat("SoundVol", Mathf.Lerp (-80, 0, volume ));
    }
}
