using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch2H1 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController1 cameraController;

    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController1>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.TheSecondFloor();
        }
    }
}