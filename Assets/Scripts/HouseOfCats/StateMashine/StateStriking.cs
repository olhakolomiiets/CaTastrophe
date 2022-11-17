using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStriking : State
{
    public StateStriking(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = false;
        houseCat.isEating = false;
        houseCat.isHiding = false;
        houseCat.isSleeping = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isWashing = false;
        houseCat.isStriking = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.IsCatStriking()==false)
        {                
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateStriking);
    }
}