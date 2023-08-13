using UnityEngine;

public class PalletFullState : PalletBaseState
{
    private PlayerController playerController;
    private PlayerRayDetection playerRayDetection;
    private Pallet thisPallet;
    public override void EnterState(PalletStateManager pallet)
    {
        thisPallet = pallet.transform.GetComponent<Pallet>();
        thisPallet.AddStartingItemsOnPallet();
    }

    public override void UpdateState(PalletStateManager pallet)
    {
        if(thisPallet.CurrentPalletCapacity > 0)
        {
            if(playerController != null)
            {
                //Check for interact input
                if (playerController.isInteractPressed && playerRayDetection.LayerCheck("Pallet") && !playerController.isCarryingNow && !playerController.isPushingTrolleyNow)
                {
                    thisPallet.TakeItemFromPallet();
                    playerController.AddItemToHand(thisPallet._givenPalletObject);
                    playerController.isCarryingNow = true;
                    playerController.itemOnCarry = thisPallet._inPalletObject;
                }
            }
        }
        else
        {
            playerController.isOnPickupZone = false;
            pallet.SwitchState(pallet.emptyState);
        }
    }
    public override void OnCollisionEnter(PalletStateManager pallet, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player")){
            playerController = other.GetComponent<PlayerController>();
            playerRayDetection = other.GetComponent<PlayerRayDetection>();
            playerController.isOnPickupZone = true;
        }
    }
    public override void OnCollisionExit(PalletStateManager pallet, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            playerController.isOnPickupZone = false;
            playerController = null;
            playerRayDetection = null;
        }
    }
}
