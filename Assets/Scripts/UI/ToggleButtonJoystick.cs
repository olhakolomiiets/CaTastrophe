using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonJoystick : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Joystick joystick;

    void Awake()
    {
        CheckControls();
    }

    public void ToggleControls(bool isJoystickActive)
    {
        leftButton.gameObject.GetComponent<Image>().enabled = !isJoystickActive;
        rightButton.gameObject.GetComponent<Image>().enabled = !isJoystickActive;
        joystick.gameObject.SetActive(isJoystickActive);
    }

    public void CheckControls()
    {
        if (PlayerPrefs.GetInt("ActiveControlsPrefs") == 0)
        {
            ToggleControls(true);
        }
        else { ToggleControls(false); }
    }
}
