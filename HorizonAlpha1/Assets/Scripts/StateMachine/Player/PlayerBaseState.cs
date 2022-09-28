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

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.Force.Movement) * deltaTime);
    }
}
