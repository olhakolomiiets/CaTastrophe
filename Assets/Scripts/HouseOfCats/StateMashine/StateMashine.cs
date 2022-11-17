using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private State currentState;
    private Dictionary<Type, State> allStates;
    public bool printStates;
    public void SetUpStates(Dictionary<Type, State> states, Type defaultState)
    {
        allStates = states;
        currentState = allStates[defaultState];
        currentState.EnterState();
    } 
    private void Update()
    {
        if (currentState == null) return;    
        Type nextState = currentState.UpdateState();
        if (nextState != currentState.GetType())
        {
            SwitchState(allStates[nextState]);
        }  
        if (printStates)
            print(currentState);
    }
    private void SwitchState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
