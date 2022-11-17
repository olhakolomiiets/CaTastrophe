using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHide : State
{
    public StateHide(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = false;
        houseCat.isSleeping = false;
        houseCat.isEating = false;
        houseCat.isWashing = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking= false;
        houseCat.isHiding = true;
    }
    public override Type UpdateState()
    {
        
        if (houseCat.hideTimer < 0)
        {
            houseCat.animFinished = true;
            Type priorityState = base.UpdateState();
            houseCat.animFinished = false;
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateHide);
    }
}
