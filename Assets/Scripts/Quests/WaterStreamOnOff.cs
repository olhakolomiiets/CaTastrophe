using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class WaterStreamOnOff : MonoBehaviour
{

    private bool isOn = false;
    private Animator[] bull;
    private List<Animator> animlist;
    public Button btn;
    private GameObject btnActive;
    private ScoreManager sm;
    private AudioSource source;
    private GameObject water;
    bool triggered = false;

    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        sm = FindObjectOfType<ScoreManager>();
        btnActive = btn.transform.GetChild(0).gameObject;
        water =  gameObject.transform.GetChild(0).gameObject;     
    }
        // private void Update()
        // {
        //      if (triggered)
        //     {
        //        btn.onClick.AddListener(Do);
                   
        //     }
        //     else if (!triggered)
        //     {
            
        //     btn.onClick.RemoveListener(Do);
            
        //     }
        // }
    public void OnTriggerEnter2D(Collider2D other)
    {     
            if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
            {
btn.onClick.AddListener(Do);
                btnActive.SetActive(true);
                 triggered = true;
           }
    }
    public void Do()
    {
    btn.GetComponent<StopMoveForDo>().StopMove();
    Switcher();    
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ActiveCollaider") | other.CompareTag("ActiveCollaiderHeavy"))
        {
                triggered = false;
               btnActive.SetActive(false);
                   btn.onClick.RemoveListener(Do);
        }
    }
    private void  Switcher () {
switch (isOn)
        {
        case true:
                Debug.Log("Here is Off");
        water.SetActive(false);
        isOn = false;  
            break;
        case false:
                 Debug.Log("Here is On");
        water.SetActive(true);
        isOn = true;
            break;
        default:
        water.SetActive(false);
        isOn = false;  
            break;
        }  
    }


}


