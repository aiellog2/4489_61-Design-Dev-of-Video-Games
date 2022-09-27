using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{

    private BaseState currentState;

    public void SwitchState(BaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }


    // Update is called once per frame
    void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
