using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePoop : State
{
    public StatePoop(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = false;
        houseCat.isSleeping = false;
        houseCat.isHiding = false;
        houseCat.isWashing = false;
        houseCat.isEating = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking= false;
        houseCat.isPooping = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.poopTimer < 0)
        {
        Type priorityState = base.UpdateState();
        if (priorityState != null)
            return priorityState;
        }
        return typeof(StatePoop);
    }
}
