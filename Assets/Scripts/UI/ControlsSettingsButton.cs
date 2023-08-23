using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _imageV;

    private void Awake()
    {
        //if (!PlayerPrefs.HasKey("ActiveControlsPrefs"))
        //{
        //    PlayerPrefs.SetInt("ActiveControlsPrefs", 1);
        //}
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("ActiveControlsPrefs") == 0)
        {
            _imageV.SetActive(true);
        }
        else
        {
            _imageV.SetActive(false);
        }
    }

    public void ControlsToggle()
    {
        if (PlayerPrefs.GetInt("ActiveControlsPrefs") == 0)
        {
            _imageV.SetActive(false);
            PlayerPrefs.SetInt("ActiveControlsPrefs", 1);
            SoundManager.snd.PlayButtonsSound();
        }
        else
        {
            _imageV.SetActive(true);
            PlayerPrefs.SetInt("ActiveControlsPrefs", 0);
            SoundManager.snd.PlayButtonsSound();
        }
    }

}
