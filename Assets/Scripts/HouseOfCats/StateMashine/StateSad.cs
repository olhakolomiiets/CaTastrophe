using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSad : State
{
    public StateSad(HouseCat connectedCat) : base(connectedCat)
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
        houseCat.isStriking = false;
        houseCat.isWashing = false;
        houseCat.isSad = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.sadTimer <= 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateSad);
    }

}
