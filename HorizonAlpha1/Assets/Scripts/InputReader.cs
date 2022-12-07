using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private StaminaBar staminaBar;
    public bool isAttacking { get; private set; }
    public bool isSprinting { get; private set; }
    public bool isBlocking { get; private set; }
    public bool isInteracting { get; private set; }




    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) { return; }
        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isAttacking = true;
        }
        else if (context.canceled)
        {
            isAttacking = false;
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }
    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBlocking = true;
        }
        else if (context.canceled)
        {
            isBlocking = false;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (controls.Player.Interact.triggered && controls.Player.Interact.ReadValue<float>() > 0)
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }
}