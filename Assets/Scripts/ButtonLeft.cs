using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLeft : EventTrigger
{
    private CowController controller;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (eventData.button != PointerEventData.InputButton.Left) return;
        controller.OnLeftButtonDown();
    }
}


































//     private CowController controller;

//     void Start()
//     {
//         controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CowController>();
//     }


//     void EventAdd (){
//     // ....
//     EventTrigger eventTrigger1 = GetComponent<EventTrigger> ();
//     EventTrigger.Entry entry = new EventTrigger.Entry( );
//     entry.eventID = EventTriggerType.PointerDown;
//     entry.callback.AddListener( ( data ) => { OnPointerDownDelegate( (PointerEventData)data ); } );
//     eventTrigger1.triggers.Add( entry );
// }

// public void OnPointerDownDelegate( PointerEventData data )
// {
//     Debug.Log( "OnPointerDownDelegate called." );
// }


//  }




//     EventTrigger trigger = ButtonLeft.GetComponent<EventTrigger>();
//     EventTrigger.Entry entry = new EventTrigger.Entry();
//     entry.eventID = EventTriggerType.PointerDown;
//     entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
//     trigger.triggers.Add(entry);


// public void OnPointerDownDelegate(PointerEventData data)
// {
//     controller.OnLeftButtonDown();  
// }
