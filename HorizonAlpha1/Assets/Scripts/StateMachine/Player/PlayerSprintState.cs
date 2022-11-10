using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprintState : PlayerBaseState
{
    public StaminaBar staminaBar;
    private readonly int SprintHash = Animator.StringToHash("Sprint");

    private const float CrossFadeDuration = 0.1f;

    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.TargetEvent += OnTarget;

        stateMachine.Animator.CrossFadeInFixedTime(SprintHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.isSprinting)
        {
            staminaBar.DecreaseStamina();
        }
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.SprintMovementSpeed, deltaTime);
        MovementDirection(movement, deltaTime);
        if(!stateMachine.InputReader.isSprinting)
        {
            stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            return;
        }
    }
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }
    private void MovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
        stateMachine.transform.rotation,
        Quaternion.LookRotation(movement),
        deltaTime * stateMachine.RotationSmoothValue);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
}
