using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}
