using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    private Vector3 _defaultJoystickPosition;
    private Image _imgBackground;
    private Image _imgHandle;

    protected override void Start()
    {
        base.Start();
        _defaultJoystickPosition = background.gameObject.transform.position;

        _imgBackground = background.GetComponent<Image>();
        _imgHandle = handle.GetComponent<Image>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _imgBackground.color = new Color(_imgBackground.color.r, _imgBackground.color.g, _imgBackground.color.b, 1f);
        _imgHandle.color = new Color(_imgHandle.color.r, _imgHandle.color.g, _imgHandle.color.b, 1f);

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _imgBackground.color = new Color(_imgBackground.color.r, _imgBackground.color.g, _imgBackground.color.b, 0.6f);
        _imgHandle.color = new Color(_imgHandle.color.r, _imgHandle.color.g, _imgHandle.color.b, 0.6f);

        background.gameObject.transform.position = _defaultJoystickPosition;
        base.OnPointerUp(eventData);
    }

}