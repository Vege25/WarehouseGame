using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleySideManager : MonoBehaviour
{
    public bool isPlayerInZone;
    public bool isRightCollider;
    [SerializeField] GameObject rightColliderObject, leftColliderObject;

    public void UpdateTrolleyColliderInfo(bool isRight, bool isInZone)
    {
        isRightCollider = isRight;
        isPlayerInZone = isInZone;
    }
}
