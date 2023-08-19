using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    public int CurrentTrolleyCapacity;
    public int MaxTrolleyCapacity;

    [SerializeField] private int currentRightCapacity, currentLeftCapacity;

    [SerializeField] private GameObject _itemPositionsOnTrolleyParent_R;
    [SerializeField] private GameObject _itemPositionsOnTrolleyParent_L;
    [SerializeField] private List<Transform> _itemPositionsOnTrolley;
    [SerializeField] private List<GameObject> _itemsOnTrolley;


    public bool isTrolleyPushed;
    public bool isDropSpotValid;

    [SerializeField] CameraPerspectiveController cameraPerspectiveController;
    [SerializeField] private Transform trolleyPosOnPlayer;
    [SerializeField] GameObject colliderObject;
    TrolleySideManager trolleySideManager;
    // Start is called before the first frame update
    void Start()
    {
        MaxTrolleyCapacity = _itemPositionsOnTrolleyParent_R.transform.childCount + _itemPositionsOnTrolleyParent_L.transform.childCount;
        CurrentTrolleyCapacity = 0;
        currentRightCapacity = 0;
        currentLeftCapacity = 0;

        trolleySideManager = GetComponent<TrolleySideManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrolleyPushed)
        {
            PushTrolley();
        }
        else
        {
            StopPushingTrolley();
        }
    }

    private void StopPushingTrolley()
    {
        transform.parent = null;
        foreach (BoxCollider collider in colliderObject.GetComponents<BoxCollider>())
        {
            collider.isTrigger = false;
        }
    }

    private void PushTrolley()
    {
        float trolleyRotation;
        if (trolleySideManager.isRightCollider){ trolleyRotation = 90.0f; } else { trolleyRotation = -90.0f; }

        transform.parent = trolleyPosOnPlayer;
        transform.position = trolleyPosOnPlayer.position;
        transform.rotation = trolleyPosOnPlayer.rotation * Quaternion.Euler(0f, trolleyRotation, 0f);
        foreach (BoxCollider collider in colliderObject.GetComponents<BoxCollider>())
        {
            collider.isTrigger = true;
        }
    }

    public void PutItemToTrolley(GameObject item, PlayerController playerController)
    {
        if (trolleySideManager.isRightCollider && currentRightCapacity < (MaxTrolleyCapacity/2))
        {
            CurrentTrolleyCapacity++;
            currentRightCapacity++;
            //int lastItem = _itemsOnTrolley.Count;
            _itemsOnTrolley.Add(item);

            GameObject nextPositionObject = _itemPositionsOnTrolleyParent_R.transform.GetChild(currentRightCapacity - 1).gameObject;
            Instantiate(item, nextPositionObject.transform.position, nextPositionObject.transform.rotation, nextPositionObject.transform);
            playerController.RemoveItemFromHand();
        }
        else if(!trolleySideManager.isRightCollider && currentLeftCapacity < (MaxTrolleyCapacity / 2))
        {
            CurrentTrolleyCapacity++;
            currentLeftCapacity++;
            //int lastItem = _itemsOnTrolley.Count;
            _itemsOnTrolley.Add(item);

            GameObject nextPositionObject = _itemPositionsOnTrolleyParent_L.transform.GetChild(currentLeftCapacity - 1).gameObject;
            Instantiate(item, nextPositionObject.transform.position, nextPositionObject.transform.rotation, nextPositionObject.transform);
            playerController.RemoveItemFromHand();
        }
        
    }

    public void MoveCameraToSideView(bool isSideViewActivated)
    {
        if (!isSideViewActivated) 
        {
            cameraPerspectiveController.trolleyRightSideCamera = false;
            cameraPerspectiveController.trolleyLeftSideCamera = false;
            return; 
        }

        if (trolleySideManager.isRightCollider)
        {
            Debug.Log("Here2");
            cameraPerspectiveController.trolleyRightSideCamera = true;
            cameraPerspectiveController.trolleyLeftSideCamera = false;
        }
        else
        {
            Debug.Log("Here3");
            cameraPerspectiveController.trolleyRightSideCamera = false;
            cameraPerspectiveController.trolleyLeftSideCamera = true;
        }
    }
}
