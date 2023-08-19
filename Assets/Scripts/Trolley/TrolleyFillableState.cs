using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyFillableState : TrolleyBaseState
{
    private Trolley thisTrolley;
    private PlayerRayDetection playerRayDetection;
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
            if(thisTrolley.CurrentTrolleyCapacity < thisTrolley.MaxTrolleyCapacity)
            {
                if (playerController.isInteractPressed && playerRayDetection.ObjectCheck(thisTrolley.gameObject) && playerController.isCarryingNow)
                {
                    thisTrolley.PutItemToTrolley(playerController.itemOnCarry, playerController);
                }
            }
            else
            {
                playerController.isOnTrolleyZone = false;
                trolley.SwitchState(trolley.fullState);
            }

            //Camera
            if (playerRayDetection.LayerCheck("Trolley"))
            {
                thisTrolley.MoveCameraToSideView(true);
            }
            else
            {
                thisTrolley.MoveCameraToSideView(false);
            }
        }
    }
    public override void OnCollisionEnter(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerRayDetection = other.GetComponent<PlayerRayDetection>();
            playerController.isOnTrolleyZone = true;
            playerRayDetection.currentItemOnCollider = thisTrolley.gameObject;
        }
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if(playerRayDetection != null && GameObject.ReferenceEquals(thisTrolley.gameObject, playerRayDetection.currentItemOnCollider))
            {
                if (playerController != null)
                {
                    playerRayDetection.currentItemOnCollider = null;
                    playerController.isOnTrolleyZone = false;
                    playerController = null;
                    playerRayDetection = null;
                    thisTrolley.MoveCameraToSideView(false);
                }
            }
        }
    }
}
