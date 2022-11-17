using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoveForDo : MonoBehaviour
{
    private CowController controller;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
    }
    public void StopMove()
    {
        controller.OnButtonUp();
    }
}
