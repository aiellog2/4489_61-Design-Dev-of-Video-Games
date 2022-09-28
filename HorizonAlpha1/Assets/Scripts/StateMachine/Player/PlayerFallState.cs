using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            return;
        }
    }
    public override void Exit()
    {

    }


}
