using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyWrappedState : TrolleyBaseState
{
    public override void EnterState(TrolleyStateManager trolley)
    {
        Debug.Log("Trolley is full and wrapped");
    }

    public override void UpdateState(TrolleyStateManager trolley)
    {

    }
    public override void OnCollisionEnter(TrolleyStateManager trolley, Collider collision)
    {
        // when interacted let trolley follow the player
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {

    }
}
