using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch2H11 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController11 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController11>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsSecondFloor();
        }
    }
}

