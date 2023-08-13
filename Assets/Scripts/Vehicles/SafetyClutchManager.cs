using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyClutchManager : MonoBehaviour
{
    [SerializeField] PlayerGameModeHanlder playerGameModeHandler;
    
    public int vehicleId = 1;
    public bool playerIsOnClutch = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Clutch engaded");
        if (other.gameObject.tag == "Player")
        {
            playerIsOnClutch = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Clutch pressure lost");
            playerIsOnClutch = false;
        }
    }
}
