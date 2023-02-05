using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibroSettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _imageV;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("VibrationSettingsPref"))
        {
            PlayerPrefs.SetInt("VibrationSettingsPref", 1);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("VibrationSettingsPref") == 1)
        {
            _imageV.SetActive(true);
        }
        else
        {
            _imageV.SetActive(false);
        }
    }

    public void VibrationToggle()
    {
        if (PlayerPrefs.GetInt("VibrationSettingsPref") == 0)
        {
            _imageV.SetActive(true);
            PlayerPrefs.SetInt("VibrationSettingsPref", 1);
            SoundManager.snd.PlayButtonsSound();
        }
        else
        {
            _imageV.SetActive(false);
            PlayerPrefs.SetInt("VibrationSettingsPref", 0);
            SoundManager.snd.PlayButtonsSound();
        }
    }

}
