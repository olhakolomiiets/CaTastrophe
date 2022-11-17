using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrink : State
{

    public StateDrink(HouseCat connectedCat) : base(connectedCat)
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
        houseCat.isEating = false;
        houseCat.isSporting = false;
        houseCat.isStriking = false;
        houseCat.isDrinking = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.drinkTimer < 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateDrink);
    }
}
