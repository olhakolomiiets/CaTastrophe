using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBarp : State
{
    public StateBarp(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = false;
        houseCat.isSleeping = false;
        houseCat.isHiding = false;
        houseCat.isWashing = false;
        houseCat.isEating = false;
        houseCat.isPooping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking = false;
        houseCat.isSad = false;
        houseCat.isBarping = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.barpTimer <= 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateBarp);
    }

}