using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : EnemyBaseState
{

    private readonly int MovementBlendTreeHash = Animator.StringToHash("Movement");
    private readonly int SpeedHash = Animator.StringToHash("Speed");


    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;


    public ChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(MovementBlendTreeHash, CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        if (!IsInDetectionRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if(InAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            return;
        }

        ChasePlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void ChasePlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MoveSpeed, deltaTime);
        }
       
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    private bool InAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
