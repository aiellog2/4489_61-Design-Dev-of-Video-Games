using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("IdleBlock");
    private const float CrossFadeDuration = 0.1f;



    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Health.SetBlocking(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (stateMachine.InputReader.isBlocking)
        {
            stateMachine.StaminaBar.DecreaseStamina();
            if (stateMachine.StaminaBar.stamina <= 0)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                return;
            }
        }

        if (!stateMachine.InputReader.isBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            return;
        }
    }
    public override void Exit()
    {
        stateMachine.Health.SetBlocking(false);
    }
}
