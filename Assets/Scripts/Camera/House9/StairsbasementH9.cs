using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsbasementH9 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController9 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController9>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cameraController.StairsBasement();
    }

}