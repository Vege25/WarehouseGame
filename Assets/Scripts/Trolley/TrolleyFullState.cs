using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyFullState : TrolleyBaseState
{
    private PlayerRayDetection playerRayDetection;
    private PlayerController playerController;
    public override void EnterState(TrolleyStateManager trolley)
    {
        Debug.Log("Trolley is full");
    }

    public override void UpdateState(TrolleyStateManager trolley)
    {

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
    }
    public override void OnCollisionExit(TrolleyStateManager trolley, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if (playerController != null)
            {
                playerController.isOnTrolleyZone = false;
                playerController = null;
                playerRayDetection = null;
            }
        }
    }
}
