using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class QuestExplainer : MonoBehaviour
{

    private GameObject Tip;
    public GameObject Canvas;
    bool triggered = false;
    public string explainerName;
    private CowController controller;
    
    void Start()
    {      
        Tip = gameObject.transform.GetChild(0).gameObject;  
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();    
    }

    private void Update()
    {
        if (triggered)
        {

            if (PlayerPrefs.GetInt(explainerName) == 0)
            {
                Tip.SetActive(true);
                Canvas.SetActive(false);
                Time.timeScale = 0;
                PlayerPrefs.SetInt(explainerName, 1);
                controller.OnButtonUp();

            }
        }
    }  

    public void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy") && PlayerPrefs.GetInt(explainerName) == 0)
            {
                triggered = true;
            }
     }

    public void Resume ()
    {
         Time.timeScale = 1;
        controller.isUiJumpPressed = false;
        controller.isJumping = false;
        Canvas.SetActive(true);
    }    
        
 }

    
// PlateExplainerDone


