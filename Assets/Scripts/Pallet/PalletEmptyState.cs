using UnityEngine;

public class PalletEmptyState : PalletBaseState
{
    public override void EnterState(PalletStateManager pallet)
    {
        //Create ticket to pallet refill
        Debug.Log("Pallet refill ticket sent");
    }

    public override void UpdateState(PalletStateManager pallet)
    {

    }
    public override void OnCollisionEnter(PalletStateManager pallet, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pallet refilled");
        }
    }
    public override void OnCollisionExit(PalletStateManager pallet, Collider collision)
    {
        
    }
}
