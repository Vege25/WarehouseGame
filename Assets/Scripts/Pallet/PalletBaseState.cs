using UnityEngine;

public abstract class PalletBaseState
{
    public abstract void EnterState(PalletStateManager pallet);

    public abstract void UpdateState(PalletStateManager pallet);

    public abstract void OnCollisionEnter(PalletStateManager pallet, Collider collision);
    public abstract void OnCollisionExit(PalletStateManager pallet, Collider collision);
}
