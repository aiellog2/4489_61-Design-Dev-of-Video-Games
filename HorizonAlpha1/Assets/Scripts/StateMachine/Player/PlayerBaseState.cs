using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : BaseState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
