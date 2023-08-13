using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletStateManager : MonoBehaviour
{
    PalletBaseState currentState;
    public PalletFullState fullState = new PalletFullState();
    public PalletBaseState emptyState = new PalletEmptyState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = fullState;

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

    public void SwitchState(PalletBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
