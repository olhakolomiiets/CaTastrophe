﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsbasementH5 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController5 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController5>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cameraController.StairsBasement();
    }

}