using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch2H5 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController5 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController5>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsSecondFloor();
        }
    }
}

