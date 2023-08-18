using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public int maxTrolleysAmount;
    public int currentTrolleysAttached;
    public bool isOnVehicleZone;

    [SerializeField] GameObject trolleysHolder;

    private PlayerInput playerInput;

    [SerializeField] private bool isMovementPressed;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.VehicleControls.Move.started += onMovementInput;
        playerInput.VehicleControls.Move.canceled += onMovementInput;
        playerInput.VehicleControls.Move.performed += onMovementInput;
    }

    private void Start()
    {
        maxTrolleysAmount = trolleysHolder.transform.childCount;
    }

    private void onMovementInput(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    public void AttachTrolleyToVehicle(GameObject trolley)
    {
        Transform nextTrolleyPosChild = trolleysHolder.transform.GetChild(currentTrolleysAttached);

        trolley.transform.position = nextTrolleyPosChild.transform.position;
        trolley.transform.rotation = Quaternion.identity;

        currentTrolleysAttached++;
    }

    private void OnEnable()
    {
        playerInput.VehicleControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.VehicleControls.Disable();
    }
}
