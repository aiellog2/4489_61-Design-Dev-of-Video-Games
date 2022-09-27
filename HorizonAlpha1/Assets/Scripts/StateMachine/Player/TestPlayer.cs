using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : PlayerBaseState
{

    
    public TestPlayer(PlayerStateMachine stateMachine) : base(stateMachine) { }
 
    public override void Enter()
    {
        
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        stateMachine.Controller.Move(movement * stateMachine.FreeMovementSpeed * deltaTime);

        if(stateMachine.InputReader.MovementValue == Vector2.zero) { return; }

        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }
    public override void Exit()
    {

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
}
