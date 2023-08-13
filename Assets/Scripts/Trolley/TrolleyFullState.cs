using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyFullState : TrolleyBaseState
{
    public override void EnterState(TrolleyStateManager trolley)
    {
        Debug.Log("Trolley is full");
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
