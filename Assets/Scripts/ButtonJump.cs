﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CowController controller;
    // public bool buttonPressed;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        // Button buttonjump = gameObject.GetComponent<Button>();
        // buttonjump.onClick.AddListener(controller.OnJumpbuttonDown);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        controller.isUiJumpPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.isUiJumpPressed = false;
        controller.isJumping = false;
    }
}