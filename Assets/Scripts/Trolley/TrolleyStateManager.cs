using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyStateManager : MonoBehaviour
{
    TrolleyBaseState currentState;
    public TrolleyEmptyState emptyState = new TrolleyEmptyState();
    public TrolleyFillableState fillableState = new TrolleyFillableState();
    public TrolleyFullState fullState = new TrolleyFullState();
    public TrolleyWrappedState wrappedState = new TrolleyWrappedState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = emptyState;

        currentState.EnterState(this);
    }

    void OnTriggerEnter(Collider collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    void OnTriggerExit(Collider collision)
    {
        currentState.OnCollisionExit(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TrolleyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
