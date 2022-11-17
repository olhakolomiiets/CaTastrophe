using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonDo : MonoBehaviour
{
  private CowController controller;
      private GameObject buttonDoLink;
      private GameObject buttonLeftLink;
      private GameObject buttonRightLink;
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
        buttonDoLink = this.gameObject.transform.GetChild(0).gameObject;
        Button buttonDoLink2 = buttonDoLink.GetComponent<Button>();
        buttonDoLink2.onClick.AddListener(controller.OnJumpbuttonDown);

        buttonLeftLink = this.gameObject.transform.GetChild(2).gameObject;
        EventTrigger buttonLeftLink2 = buttonLeftLink.GetComponent<EventTrigger>();
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { controller.OnLeftButtonDown(); });
        buttonLeftLink2.triggers.Add(entry); 
  
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerUp;
        entry2.callback.AddListener((data) => { controller.OnButtonUp(); });
        buttonLeftLink2.triggers.Add(entry2);        
        buttonRightLink = this.gameObject.transform.GetChild(3).gameObject;
        EventTrigger buttonRightLink2 = buttonRightLink.GetComponent<EventTrigger>();
        
        EventTrigger.Entry entryR = new EventTrigger.Entry();
        entryR.eventID = EventTriggerType.PointerDown;
        entryR.callback.AddListener((data) => { controller.OnRightButtonDown(); });
        buttonRightLink2.triggers.Add(entryR); 
  
        EventTrigger.Entry entryR2 = new EventTrigger.Entry();
        entryR2.eventID = EventTriggerType.PointerUp;
        entryR2.callback.AddListener((data) => { controller.OnButtonUp(); });
        buttonRightLink2.triggers.Add(entryR2);      
    }
}

    
