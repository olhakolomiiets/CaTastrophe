using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof (Slider))]
public class SliderSound : MonoBehaviour
{
Slider slider 
{
    get {return GetComponent<Slider>();}
}

[Header("Volume Name")]
  public AudioMixer mixer;
  
   [SerializeField]
   private string volumeName = "Enter Volume Name Here";
   [Header("Volume Label")]
    [SerializeField]
   private Text volumeLabel;
   private void Start() {
       ResetSliderValue();
       
       slider.onValueChanged.AddListener(delegate
       {
           UpdateValueOnChange(slider.value);
       });
   }


    public void UpdateValueOnChange (float value)
    {
       
        if(volumeLabel !=null)
            volumeLabel.text = Mathf.Round (value * 100.0f).ToString()+"%";
            if (Settings.profile)
            {
                Settings.profile.SetAudioLevels(volumeName,value);
            }
    }

    public void ResetSliderValue ()
    {
        if(Settings.profile)
        {
            float volume = Settings.profile.GetAudioLevels(volumeName);
            UpdateValueOnChange(volume);
            slider.value = volume;
        }
    }

    
}
