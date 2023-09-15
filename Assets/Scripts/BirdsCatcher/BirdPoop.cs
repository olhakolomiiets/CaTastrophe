using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPoop : MonoBehaviour
{
    private CowController controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.StartWashing();
            this.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Floor"))
        { 
            this.gameObject.SetActive(false);
        }
    }
}
