using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGameModeHanlder : MonoBehaviour
{
    public bool isDriving = false;
    public int ownedVehicleId;


    private PlayerInput playerInput;
    private bool isInteractPressed;

    [SerializeField] SafetyClutchManager safetyClutchManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] VehicleController vehicleController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject vehicle;
    // Start is called before the first frame update

    private void Awake()
    {
        playerInput = playerController.playerInput;

        playerInput.CharacterControls.Interact.started += interact;
        //playerInput.CharacterControls.Interact.canceled += interact;
    }

    private void interact(InputAction.CallbackContext ctx)
    {
        isInteractPressed = ctx.ReadValueAsButton();
        Debug.Log(isInteractPressed);
        CheckVehicleInteraction();
    }

    private void CheckVehicleInteraction()
    {
        if (safetyClutchManager.playerIsOnClutch && !isDriving && safetyClutchManager.vehicleId == ownedVehicleId)
        {
            player.transform.parent = vehicle.transform;
            playerController.enabled = false;
            vehicleController.enabled = true;
            isDriving = true;
        }
        else if(safetyClutchManager.playerIsOnClutch && isDriving)
        {
            player.transform.parent = vehicle.transform;
            playerController.enabled = true;
            vehicleController.enabled = false;
            isDriving = false;
        }
    }
}
