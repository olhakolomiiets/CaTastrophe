using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonJumpRunner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RunnerPlayer controller;
    private Button jumpButton;
    private void Start()
    {
        jumpButton = GetComponent<Button>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(jumpButton.interactable)
        {
            controller.isUiJumpPressed = true;
        }       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.isUiJumpPressed = false;
    }
}