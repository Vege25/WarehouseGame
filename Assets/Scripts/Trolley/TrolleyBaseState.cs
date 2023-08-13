using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrolleyBaseState
{
    public abstract void EnterState(TrolleyStateManager trolley);

    public abstract void UpdateState(TrolleyStateManager trolley);

    public abstract void OnCollisionEnter(TrolleyStateManager trolley, Collider collision);
    public abstract void OnCollisionExit(TrolleyStateManager trolley, Collider collision);
}
