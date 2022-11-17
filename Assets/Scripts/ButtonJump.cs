using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJump : MonoBehaviour
{
    private CowController controller;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        Button buttonjump = gameObject.GetComponent<Button>();
        buttonjump.onClick.AddListener(controller.OnJumpbuttonDown);
    }
}