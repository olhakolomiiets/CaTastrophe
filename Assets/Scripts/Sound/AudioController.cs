using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private ProfilesSound m_profiles;
    [SerializeField]
    private List<SliderSound> m_VolumeSliders = new List<SliderSound> ();

  
    private void Awake() {
        if(m_profiles!=null)
        m_profiles.SetProfile(m_profiles);
            }
    void Start()
    {
        if (Settings.profile && Settings.profile.audioMixer !=null)
        Settings.profile.GetAudioLevels();
    }

    // Update is called once per frame
    public void ApplyChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer !=null)
        Settings.profile.SaveAudioLevels();
    }

    public void CancelChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer !=null)
        Settings.profile.GetAudioLevels();
        for (int i = 0; i < m_VolumeSliders.Count; i++)
        {
            m_VolumeSliders[i].ResetSliderValue();
        }
    }
}
