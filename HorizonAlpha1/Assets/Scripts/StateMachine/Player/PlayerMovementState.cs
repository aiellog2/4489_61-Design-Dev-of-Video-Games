using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementState : PlayerBaseState
{
    private bool shouldFade;

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public StaminaBar staminaBar;

    public PlayerMovementState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine) 
    {
        this.shouldFade = shouldFade;
    }
 
    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.TargetEvent += OnTarget;

        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, 0.1f);
    }
    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new AttackState(stateMachine, 0));
            return;
        }

        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeMovementSpeed, deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {        
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);    
            return;

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(movement * stateMachine.SprintMovementSpeed, deltaTime);
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            MovementDirection(movement, deltaTime);
            staminaBar.DecreaseStamina();
        }
        else if(staminaBar.stamina != staminaBar.maxStamina)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.5f, AnimatorDampTime, deltaTime);
            MovementDirection(movement, deltaTime);
            staminaBar.IncreaseStamina();
        }
        staminaBar.staminaBarUI.value = staminaBar.stamina;
    }
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }
    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
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
}
