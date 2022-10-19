using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetingState : PlayerBaseState
{

    private readonly int TargetBlendTreeHash = Animator.StringToHash("TargetBlendTree");

    private readonly int TargetFowardHash = Animator.StringToHash("TargetingForward");

    private readonly int TargetRightHash = Animator.StringToHash("TargetingRight");

    public TargetingState(PlayerStateMachine stateMachine) : base(stateMachine){ }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        
        stateMachine.Animator.CrossFadeInFixedTime(TargetBlendTreeHash, 0.1f);

    }
    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new AttackState(stateMachine, 0));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        updateAnimator(deltaTime);

        FaceTarget();
    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void updateAnimator(float deltatime)
    {
        if(stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetFowardHash, 0, 0.1f, deltatime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetFowardHash, value, 0.1f, deltatime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetRightHash, 0, 0.1f, deltatime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetRightHash, value, 0.1f, deltatime);
        }
    }

}
