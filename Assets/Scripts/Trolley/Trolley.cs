using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trolley : MonoBehaviour
{
    public int CurrentTrolleyCapacity;
    public int MaxTrolleyCapacity;

    [SerializeField] private int _palletHallwayLocation, _palletZoneLocation, _palletLocation;

    [SerializeField] private GameObject _itemPositionsOnTrolleyParent;
    [SerializeField] private List<Transform> _itemPositionsOnTrolley;
    [SerializeField] private List<GameObject> _itemsOnTrolley;


    public bool isTrolleyPushed;
    public bool isTouchingVehicle; //TODO
    [SerializeField] private Transform trolleyPosOnPlayer;
    [SerializeField] GameObject colliderObject;
    // Start is called before the first frame update
    void Start()
    {
        MaxTrolleyCapacity = _itemPositionsOnTrolleyParent.transform.childCount;
        CurrentTrolleyCapacity = 0;
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
            collider.enabled = true;
        }
    }

    private void PushTrolley()
    {
        transform.parent = trolleyPosOnPlayer;
        transform.position = trolleyPosOnPlayer.position;
        transform.rotation = trolleyPosOnPlayer.rotation * Quaternion.Euler(0f, 90f, 0f);
        foreach (BoxCollider collider in colliderObject.GetComponents<BoxCollider>())
        {
            collider.enabled = false;
        }
    }

    public void PutItemToTrolley(GameObject item)
    {
            CurrentTrolleyCapacity++;
            int lastItem = _itemsOnTrolley.Count;
            _itemsOnTrolley.Add(item);
            GameObject nextPositionObject = _itemPositionsOnTrolleyParent.transform.GetChild(lastItem).gameObject;
            Instantiate(item, nextPositionObject.transform.position, nextPositionObject.transform.rotation, nextPositionObject.transform);
    }
}
