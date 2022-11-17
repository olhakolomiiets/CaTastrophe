using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch2H6 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController6 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController6>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsSecondFloor();
        }
    }
}

