using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch1H2 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController2 cameraController;

    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController2>();
    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheFirstFloor();
        }
    }
}
