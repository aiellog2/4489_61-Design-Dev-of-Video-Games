using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");

    private const float CrossFadeDuration = 0.1f;

    private Vector3 momentum;

    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(stateMachine.Controller.isGrounded)
        {
            BacktoMove();
        }
    }
    public override void Exit()
    {

    }
}
