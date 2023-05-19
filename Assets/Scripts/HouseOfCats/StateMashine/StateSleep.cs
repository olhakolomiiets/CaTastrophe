using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSleep : State
{
    public StateSleep(HouseCat connectedCat) : base(connectedCat)
    {
    }
    public override void EnterState()
    {
        houseCat.jump = false;
        houseCat.isEating = false;
        houseCat.isHiding = false;
        houseCat.isWashing = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking= false;
        houseCat.isSad = false;
        houseCat.isSleeping = true;
    }
    public override Type UpdateState()
    {       
        if (houseCat.sleepTimer < 0)
        {          
            return typeof(StatePlaying);  
            // Type priorityState = base.UpdateState();
            // if (priorityState != null)
            //     return priorityState;
        }
        return typeof(StateSleep);
    }
}
