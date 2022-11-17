using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEat : State
{

    public StateEat(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = true;
        houseCat.isSleeping = false;
        houseCat.isHiding = false;
        houseCat.isWashing = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking = false;
        houseCat.isEating = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.eatTimer < 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateEat);
    }
}
