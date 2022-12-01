using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class AttackState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private AttackStats attack;
public AttackState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackId];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {

        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                ApplyForce();
            }

            if (stateMachine.InputReader.isAttacking)
            {
                ComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerMovementState(stateMachine));
            }
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }
    private void ComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTimer) { return; }

        stateMachine.SwitchState
        (
            new AttackState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    private void ApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.Force.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;

    }
}
