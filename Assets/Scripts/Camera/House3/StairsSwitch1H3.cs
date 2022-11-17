using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch1H3 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController3 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController3>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsFirstFloor();
        }
    }
}