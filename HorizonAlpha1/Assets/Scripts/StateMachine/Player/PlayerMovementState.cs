using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Timeline.Actions;
using UnityEditor.ShaderGraph;
using UnityEngine.SceneManagement;

public class PlayerMovementState : PlayerBaseState
{

    public Controls controls;
    private bool shouldFade;

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");


    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    private int scene = SceneManager.GetActiveScene().buildIndex;

  /*  public void Start()
    {
       if (scene > 0);
      {
        Debug.Log("Not on level 1");
      }
    }*/

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
        stateMachine.StaminaBar.IncreaseStamina();
        if (scene == 0)
        {
          if (stateMachine.NPC.playerCollided == true && Input.GetKeyDown(KeyCode.E))
          {
              stateMachine.Wall.SetActive(false);
              stateMachine.Wall1.SetActive(false);
              stateMachine.SwitchState(new PlayerDialogState(stateMachine));
          }
        }


        if (stateMachine.InputReader.isAttacking && stateMachine.StaminaBar.stamina > 0)
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

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

            if (stateMachine.InputReader.isSprinting && stateMachine.StaminaBar.stamina > 0)
            {
                stateMachine.SwitchState(new PlayerSprintState(stateMachine));
                return;
            }

        MovementDirection(movement, deltaTime);

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
