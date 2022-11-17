using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaying : State
{
    public StatePlaying(HouseCat connectedCat) : base(connectedCat)
    {
    }

    public override void EnterState()
    {
        houseCat.isEating = false;
        houseCat.isHiding = false;
        houseCat.isSleeping = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isWashing = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking = false;
        houseCat.isPlaying = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.playTimer <= 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StatePlaying);
    }
         public override void ExitState()
    {
        houseCat.animFinished = true;
    }
}