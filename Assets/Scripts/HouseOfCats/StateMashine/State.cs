using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected HouseCat houseCat;
    public int stateIndex;
    public State(HouseCat connectedCat)
    {
        houseCat = connectedCat;
    }
    public virtual void EnterState()
    {
    }
    public virtual Type UpdateState()
    {
        if (houseCat.IsCatStriking() == false)
        {
            if (houseCat.animFinished)
            {
                houseCat.animFinished = false;
                stateIndex = UnityEngine.Random.Range(1, 10);
                switch (stateIndex)
                {
                    case 1:
                        houseCat.hideTimer = UnityEngine.Random.Range(6, 12);
                        return typeof(StateHide);
                    case 2:
                        houseCat.eatTimer = UnityEngine.Random.Range(13, 18);
                        return typeof(StateEat);
                    case 3:
                        houseCat.sleepTimer = UnityEngine.Random.Range(7, 16);
                        return typeof(StateSleep);
                    case 4:
                        houseCat.washTimer = UnityEngine.Random.Range(13, 18);
                        return typeof(StateWashing);
                    case 5:
                        houseCat.poopTimer = UnityEngine.Random.Range(3, 3);
                        return typeof(StatePoop);
                    case 6:
                        houseCat.barpTimer = UnityEngine.Random.Range(3,3);
                        return typeof(StateBarp);
                    case 7:
                        houseCat.playTimer = UnityEngine.Random.Range(6, 13);
                        return typeof(StatePlaying);
                    case 8:
                        houseCat.drinkTimer = UnityEngine.Random.Range(9, 12);
                        return typeof(StateDrink);
                    case 9:
                        houseCat.sportTimer = UnityEngine.Random.Range(4, 9);
                        return typeof(StateSport);
                    default:
                        houseCat.sleepTimer = UnityEngine.Random.Range(7, 16);
                        return typeof(StateSleep);
                }
            }
        }
        else if (houseCat.IsCatStriking() == true)
        {
            return typeof(StateStriking);
        }
        return null;
    }
    public virtual void ExitState()
    {
    }
}