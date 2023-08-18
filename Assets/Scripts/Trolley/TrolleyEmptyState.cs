using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyEmptyState : TrolleyBaseState
{
    private PlayerController playerController;
    private VehicleController vehicleController;
    private PlayerRayDetection playerRayDetection;
    private Trolley thisTrolley;

    public override void EnterState(TrolleyStateManager trolley)
    {
        thisTrolley = trolley.transform.GetComponent<Trolley>();
        thisTrolley.isDropSpotValid = true;
    }

    public override void UpdateState(TrolleyStateManager trolley)
    {
        if (playerController != null && playerController.isOnTrolleyZone && vehicleController == null)
        {
            //Check for interact input
            if (playerController.isInteractPressed && playerRayDetection.ObjectCheck(thisTrolley.gameObject) && !playerController.isCarryingNow && !playerController.isPushingTrolleyNow && !thisTrolley.isTrolleyPushed)
            {
                thisTrolley.isTrolleyPushed = true;
                playerController.isPushingTrolleyNow = true;
                playerController.isInteractPressed = false;
            }

            if (playerController.isInteractPressed && thisTrolley.isTrolleyPushed && thisTrolley.isDropSpotValid)
            {
                thisTrolley.isTrolleyPushed = false;
                playerController.isPushingTrolleyNow = false;
                playerController.isInteractPressed = false;
            }
        }

        if (vehicleController != null && vehicleController.isOnVehicleZone && vehicleController.currentTrolleysAttached < vehicleController.maxTrolleysAmount)
        {
            Debug.Log("Here");
            if (playerController.isInteractPressed && thisTrolley.isTrolleyPushed)
            {
                thisTrolley.isTrolleyPushed = false;
                playerController.isPushingTrolleyNow = false;
                playerController.isOnTrolleyZone = false;
                playerController.isInteractPressed = false;

                vehicleController.AttachTrolleyToVehicle(thisTrolley.gameObject);

                trolley.SwitchState(trolley.fillableState);
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
        }
        else if (other.CompareTag("Vehicle"))
        {
            //thisTrolley.isDropSpotValid = false;
            if (thisTrolley.isTrolleyPushed)
            {
                vehicleController = other.GetComponent<VehicleController>();
                if (vehicleController != null)
                {
                    vehicleController.isOnVehicleZone = true;
                }
            }
        }
        /*else
        {
            thisTrolley.isDropSpotValid = false;
        }*/

        
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if(playerController != null)
            {
                playerController.isOnTrolleyZone = false;
            }
            playerController = null;
            playerRayDetection = null;
        }
        else if (other.CompareTag("Vehicle"))
        {
            if(vehicleController != null)
            {
                vehicleController.isOnVehicleZone = false;
                vehicleController = null;
            }
        }
        /*else
        {
            thisTrolley.isDropSpotValid = true;
        }*/
    }
}
