using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSwitch1H9 : MonoBehaviour
{
    public GameObject cameraMain;
    private CameraController9 cameraController;
    void Start()
    {
        cameraController = cameraMain.GetComponent<CameraController9>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            cameraController.StairsFirstFloor();
        }
    }


}