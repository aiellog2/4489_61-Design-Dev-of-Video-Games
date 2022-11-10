using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachine
  {
     [field: SerializeField] public InputReader InputReader { get; private set; }
     [field: SerializeField] public CharacterController Controller { get; private set; }
     [field: SerializeField] public Targeter Targeter { get; private set; }
     [field: SerializeField] public Animator Animator { get; private set; }
     [field: SerializeField] public Damage Weapon { get; private set; }
     [field: SerializeField] public Health Health { get; private set; }
     [field: SerializeField] public StaminaBar StaminaBar { get; private set; }
     [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
     [field: SerializeField] public float FreeMovementSpeed { get; private set; }
     [field: SerializeField] public float SprintMovementSpeed { get; private set; }
     [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
     [field: SerializeField] public float RollDuration { get; private set; }
     [field: SerializeField] public float RollDistance { get; private set; }
     [field: SerializeField] public Force Force { get; private set; }
     [field: SerializeField] public float RotationSmoothValue { get; private set; }
     [field: SerializeField] public AttackStats[] Attacks { get; private set; }
     [field: SerializeField] public float JumpForce { get; private set; }
     public Transform MainCameraTransform { get; private set; }
     public float PreviousRollTime { get; private set; } = Mathf.NegativeInfinity;


    private void Start()
      {
          Cursor.visible = false;

          MainCameraTransform = Camera.main.transform;

          SwitchState(new PlayerMovementState(this));
      }

    private void OnEnable()
    {
        Health.takeDamage += tookDamage;
        Health.Die += Death;
    }
    private void OnDisable()
    {
        Health.takeDamage -= tookDamage;
        Health.Die -= Death;

    }
    private void tookDamage()
    {
        SwitchState(new PlayerHitState(this));
    }
    private void Death()
    {
        SwitchState(new PlayerDieState(this));
    }
}
