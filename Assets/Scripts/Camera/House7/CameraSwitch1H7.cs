using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch1H7 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController7 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController7>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheFirstFloor();
        }
    }
}