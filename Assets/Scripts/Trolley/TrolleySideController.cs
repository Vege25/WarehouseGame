using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleySideController : MonoBehaviour
{
    [SerializeField] bool isRightSide;
    [SerializeField] TrolleySideManager trolleySideManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trolleySideManager.UpdateTrolleyColliderInfo(isRightSide, true); //which side, is triggered
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            trolleySideManager.UpdateTrolleyColliderInfo(isRightSide, false); //which side, is triggered
        }
    }
}
