using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch1H12 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController12 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController12>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsFirstFloor();
        }
    }


}