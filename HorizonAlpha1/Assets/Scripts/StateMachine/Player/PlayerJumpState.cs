using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Force.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
    }
    public override void Tick(float deltaTime)
    {

        Move(momentum, deltaTime);

        if (stateMachine.Controller.velocity.y <= 0 || stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }
    }
    public override void Exit()
    {

    }

}
