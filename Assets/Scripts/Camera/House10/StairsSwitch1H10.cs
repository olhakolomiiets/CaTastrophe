using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch1H10 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController10 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController10>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsFirstFloor();
        }
    }


}