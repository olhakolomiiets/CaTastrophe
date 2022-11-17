using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsbasementH12 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController12 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController12>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cameraController.StairsBasement();
    }

}