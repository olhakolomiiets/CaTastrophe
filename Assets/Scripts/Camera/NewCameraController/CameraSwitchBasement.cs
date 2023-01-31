using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchBasement : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheBasement();
        }
    }
}
