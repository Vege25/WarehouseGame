using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayDetection : MonoBehaviour
{
    public GameObject CurrentObjectOnRay;
    public bool IsRayDetectionValid;

    [SerializeField] LayerMask detectLayers;
    [SerializeField] Transform raycastStartPos;
    [SerializeField] float rayDistance = 3.0f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 offset = new Vector3(0f, -0.75f, 0f);

        if (Physics.Raycast(raycastStartPos.position, transform.TransformDirection(Vector3.forward) + offset, out hit, rayDistance, detectLayers)){
            Debug.DrawRay(raycastStartPos.position, (transform.TransformDirection(Vector3.forward) + offset) * rayDistance, Color.green);
            CurrentObjectOnRay = hit.transform.gameObject;
        }
        else
        {
            Debug.DrawRay(raycastStartPos.position, (transform.TransformDirection(Vector3.forward) + offset) * rayDistance, Color.white);
            CurrentObjectOnRay = null;
        }
    }

    public bool ObjectCheck(GameObject gameObject)
    {
        if (CurrentObjectOnRay == null)
        {
            return false;
        }
        else if (GameObject.ReferenceEquals(gameObject, CurrentObjectOnRay))
        {
            return true;
        }
        else
        {
            return false;
        }
        /*LayerMask layer = CurrentObjectOnRay.layer;
        if (layer == LayerMask.NameToLayer(layerName))
        {
            return true;
        }
        else
        {
            return false;
        }*/
    }
}
