using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSport : State
{
    public StateSport(HouseCat connectedCat) : base(connectedCat)
    {
    }

    public override void EnterState()
    {
        houseCat.sportingAnim = UnityEngine.Random.Range(1, 3);
        houseCat.jump = false;
        houseCat.isEating = false;
        houseCat.isHiding = false;
        houseCat.isWashing = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSleeping = false;
        houseCat.isStriking= false;
        houseCat.isSad = false;
        houseCat.isSporting = true;

    }
    public override Type UpdateState()
    {        
        if (houseCat.sportTimer < 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateSport);
    }
}
