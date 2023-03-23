using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStates CurrentState {get; private set;}

    public void Initialize(PlayerStates startingStates)
    {
        CurrentState = startingStates;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerStates newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
