using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch1H8 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController8 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController8>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheFirstFloor();
        }
    }
}