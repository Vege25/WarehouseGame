using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyEmptyState : TrolleyBaseState
{
    private PlayerController playerController;
    private Trolley thisTrolley;
    public override void EnterState(TrolleyStateManager trolley)
    {
        thisTrolley = trolley.transform.GetComponent<Trolley>();
    }

    public override void UpdateState(TrolleyStateManager trolley)
    {
        if (playerController != null && playerController.isOnTrolleyZone)
        {
            //Check for interact input
            if (playerController.isInteractPressed && !playerController.isCarryingNow && !playerController.isPushingTrolleyNow && !thisTrolley.isTrolleyPushed)
            {
                thisTrolley.isTrolleyPushed = true;
                playerController.isPushingTrolleyNow = true;
            }

            if (!playerController.isInteractPressed && thisTrolley.isTrolleyPushed)
            {
                thisTrolley.isTrolleyPushed = false;
                playerController.isPushingTrolleyNow = false;
            }
        }

    }
    public override void OnCollisionEnter(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.isOnTrolleyZone = true;
        }

        if (other.CompareTag("Vehicle"))
        {
            if (thisTrolley.isTrolleyPushed)
            {
                thisTrolley.isTrolleyPushed = false;
                playerController.isPushingTrolleyNow = false;
                playerController.isOnTrolleyZone = false;
                trolley.SwitchState(trolley.fillableState);
            }
        }
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            playerController.isOnTrolleyZone = false;
            playerController = null;
        }
    }
}
