using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch1H4 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController4 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController4>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheFirstFloor();
        }
    }
}