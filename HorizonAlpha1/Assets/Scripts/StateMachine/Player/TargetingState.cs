using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetingState : PlayerBaseState
{
    public TargetingState(PlayerStateMachine stateMachine) : base(stateMachine){ }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.SwitchState(new PlayerMovementState(stateMachine));
    }
}
