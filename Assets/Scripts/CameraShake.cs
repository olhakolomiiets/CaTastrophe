using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;


     void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Camera.main.DOShakePosition(_shakeDuration, _shakeStrength);
    }
}
}
