using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    private readonly int hitHash = Animator.StringToHash("Hit");

    private const float CrossFadeDuration = 0.1f;

    private float duration = 1f;

    public PlayerHitState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(hitHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0f)
        {
            BacktoMove();
        }
    }
    public override void Exit()
    {

    }
}
