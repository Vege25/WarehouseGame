using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    public int CurrentPalletCapacity;

    [SerializeField] private int _maxPalletCapacity;
    public GameObject _inPalletObject;
    public GameObject _givenPalletObject;

    [SerializeField] private int _palletHallwayLocation, _palletZoneLocation, _palletLocation;

    [SerializeField] private GameObject _itemPositionsOnPalletParent;
    [SerializeField] private List<Transform> _itemPositionsOnPallet;
    [SerializeField] private List<GameObject> _itemsOnPallet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStartingItemsOnPallet()
    {
        _maxPalletCapacity = _itemPositionsOnPalletParent.transform.childCount;
        CurrentPalletCapacity = _maxPalletCapacity;

        for (int i = 0; i < _maxPalletCapacity; i++)
        {
            _itemPositionsOnPallet.Add(_itemPositionsOnPalletParent.transform.GetChild(i));//not needed

            Transform nextItemTransform = _itemPositionsOnPalletParent.transform.GetChild(i).transform;
            Instantiate(_inPalletObject, nextItemTransform.position, nextItemTransform.rotation, nextItemTransform);
            _itemsOnPallet.Add(_itemPositionsOnPallet[i].GetChild(0).gameObject); //item positions child object (The item)
        }
    }

    public void TakeItemFromPallet()
    {
        CurrentPalletCapacity--;
        int lastItem = _itemsOnPallet.Count - 1;
        _itemsOnPallet.RemoveAt(lastItem);
        GameObject lastPositionObject = _itemPositionsOnPalletParent.transform.GetChild(lastItem).gameObject;
        GameObject lastPositionObjectChild = lastPositionObject.transform.GetChild(0).gameObject;
        Destroy(lastPositionObjectChild);
    }

    public void PutItemBackToPallet()
    {
        Debug.Log("Put item back to pallet");
    }
}
