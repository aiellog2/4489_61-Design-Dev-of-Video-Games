using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Timeline.Actions;

public class PlayerDialogState : PlayerBaseState
{
    
    private readonly int NPCState = Animator.StringToHash("NPCState");

    public PlayerDialogState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.InteractEvent += OnInteract;

        stateMachine.Animator.CrossFadeInFixedTime(NPCState, 0.1f);

    }
    public override void Tick(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        stateMachine.InputReader.InteractEvent -= OnInteract;
    }
    private void OnInteract()
    {
        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
}
