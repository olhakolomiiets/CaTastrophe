using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitchBasement : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cameraController.StairsBasement();
    }
}
