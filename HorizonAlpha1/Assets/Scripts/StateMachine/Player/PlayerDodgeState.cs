using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int RollBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int RollForwardHash = Animator.StringToHash("RollForward");
    private readonly int RollRightHash = Animator.StringToHash("RollRight");

    private float remainingRollTime;
    private Vector3 dodgingDirectionInput;

    private const float CrossFadeDuration = 0.1f;

    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingRollTime = stateMachine.RollDuration;

        stateMachine.Animator.SetFloat(RollForwardHash, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(RollRightHash, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(RollBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetBlocking(true);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.RollDistance / stateMachine.RollDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.RollDistance / stateMachine.RollDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingRollTime -= deltaTime;

        if (remainingRollTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetBlocking(false);
    }

}
