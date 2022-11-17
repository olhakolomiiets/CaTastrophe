using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWashing : State
{
    public StateWashing(HouseCat connectedCat) : base(connectedCat)
    {
    }

    public override void EnterState()
    {
        houseCat.washingAnim = UnityEngine.Random.Range(1, 3);
        houseCat.jump = true;
        houseCat.isEating = false;
        houseCat.isHiding = false;
        houseCat.isSleeping = false;
        houseCat.isPooping = false;
        houseCat.isBarping = false;
        houseCat.isPlaying = false;
        houseCat.isDrinking = false;
        houseCat.isSporting = false;
        houseCat.isStriking = false;
        houseCat.isWashing = true;
    }
    public override Type UpdateState()
    {
        if (houseCat.washTimer <= 0)
        {
            Type priorityState = base.UpdateState();
            if (priorityState != null)
                return priorityState;
        }
        return typeof(StateWashing);
    }

}