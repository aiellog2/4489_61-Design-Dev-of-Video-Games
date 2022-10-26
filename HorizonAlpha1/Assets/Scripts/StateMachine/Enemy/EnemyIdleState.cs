using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int MovementBlendTreeHash = Animator.StringToHash("Movement");
    private readonly int SpeedHash = Animator.StringToHash("Speed");


    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;


    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if(IsInDetectionRange())
        {
            stateMachine.SwitchState(new ChasingState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, CrossFadeDuration, deltaTime);

    }
    public override void Exit()
    {

    }
}
