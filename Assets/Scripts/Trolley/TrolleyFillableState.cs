using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyFillableState : TrolleyBaseState
{
    private Trolley thisTrolley;
    private PlayerController playerController;
    public override void EnterState(TrolleyStateManager trolley)
    {
        thisTrolley = trolley.transform.GetComponent<Trolley>();
        Debug.Log("Ready to put stuff");

    }

    public override void UpdateState(TrolleyStateManager trolley)
    {
        if(playerController != null)
        {
            if(playerController.isInteractPressed && playerController.isCarryingNow)
            {
                thisTrolley.PutItemToTrolley(playerController.itemOnCarry);
                playerController.RemoveItemFromHand();
            }
        }
    }
    public override void OnCollisionEnter(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");
            playerController = other.GetComponent<PlayerController>();
            playerController.isOnTrolleyZone = true;
        }
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if(playerController != null)
            {
                playerController.isOnTrolleyZone = false;
                playerController = null;
            }
        }
    }
}
