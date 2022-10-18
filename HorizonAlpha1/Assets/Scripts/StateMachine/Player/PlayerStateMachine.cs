using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
   [field: SerializeField] public InputReader InputReader { get; private set; }
   [field: SerializeField] public CharacterController Controller { get; private set; }
   [field: SerializeField] public Targeter Targeter { get; private set; }
   [field: SerializeField] public Animator Animator { get; private set; }
   [field: SerializeField] public Damage Weapon { get; private set; }
   [field: SerializeField] public float FreeMovementSpeed { get; private set; }
   [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
   [field: SerializeField] public Force Force { get; private set; }
   [field: SerializeField] public float RotationSmoothValue { get; private set; }
   [field: SerializeField] public AttackStats[] Attacks { get; private set; }
   [field: SerializeField] public float JumpForce { get; private set; }
   public Transform MainCameraTransform { get; private set; }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerMovementState(this));
    }
}
