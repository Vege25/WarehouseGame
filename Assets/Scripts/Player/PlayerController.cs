using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float runSpeed = 6.0f;
    [SerializeField]
    private float walkSpeed = 3.0f;
    [SerializeField]
    private float rotationSpeed = 5f;

    private CharacterController controller;
    public PlayerInput playerInput;
    [SerializeField] private Animator animator;

    int isWalkingHash;
    int isRunningHash;
    int isCarryingHash;
    int isDancingHash;

    [SerializeField]private bool groundedPlayer;
    [SerializeField]private bool isRunPressed;
    public bool isInteractPressed;
    [SerializeField]private bool isMovementPressed;

    private bool isDancing;
    [SerializeField] float danceTimerTime = 15.0f;
    [SerializeField] float currentTimer = 0.0f;
    private bool startDanceTimer;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;

    //carrying
    public bool isCarryingNow;
    public bool isPushingTrolleyNow;
    public bool isOnPickupZone;
    public bool isOnTrolleyZone;
    public GameObject itemOnCarry;
    [SerializeField] Transform itemPos;

    private void Awake()
    {
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();
        //playerInput = GetComponent<PlayerInput>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isCarryingHash = Animator.StringToHash("isCarrying");
        isDancingHash = Animator.StringToHash("isDancing");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += ctx => isRunPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Run.canceled += ctx => isRunPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Interact.started += onInteract;
        playerInput.CharacterControls.Interact.canceled += onInteract;
    }

    void Update()
    {
        HandleGravity();
        HandleAnimation();
        MoveController();
        HandleRotation();
        HandleDanceTimer();
    }

    private void MoveController()
    {
        if (isRunPressed)
        {
            controller.Move(currentMovement * Time.deltaTime * runSpeed);
        }
        else
        {
            controller.Move(currentMovement * Time.deltaTime * walkSpeed);
        }
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void onMovementInput(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        if (!isMovementPressed && !isDancing && !isCarryingNow && !isPushingTrolleyNow)
        {
            StartDanceCountDown();
        }
        else if (isMovementPressed)
        {
            isDancing = false;
            startDanceTimer = false;
        }
    }
    private void onInteract(InputAction.CallbackContext ctx)
    {
        isInteractPressed = ctx.ReadValueAsButton();
        isDancing = false;


        if (isCarryingNow && isInteractPressed && !isOnPickupZone && !isOnTrolleyZone)
        {
            Debug.Log("Dropped the item");
            DropItem();
        }
    }

    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

        if (isDancing)
        {
            animator.SetBool(isDancingHash, true);
        }
        else{ animator.SetBool(isDancingHash, false); }

        //carrying
        if (isCarryingNow || isPushingTrolleyNow)
        {
            animator.SetBool(isCarryingHash, true);
        }
        else { animator.SetBool(isCarryingHash, false); }
    }
    void HandleGravity()
    {
        if (controller.isGrounded)
        {
            float groundedGravity = -0.05f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            currentMovement.y += gravityValue;
        }
    }

    void DropItem()
    {
        isCarryingNow = false;
        Destroy(itemPos.GetChild(0).gameObject);
        Instantiate(itemOnCarry, transform.position, Quaternion.identity, null);
        itemOnCarry = null;
    }

    public void RemoveItemFromHand()
    {
        isCarryingNow = false;
        Destroy(itemPos.GetChild(0).gameObject);
        itemOnCarry = null;
    }

    public void AddItemToHand(GameObject item)
    {
        Instantiate(item, itemPos.position, itemPos.rotation, itemPos);
    }

    private void StartDanceCountDown()
    {
        currentTimer = danceTimerTime;
        startDanceTimer = true;
    }
    void HandleDanceTimer()
    {
        if (startDanceTimer)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0.0f)
            {
                isDancing = true;
            }
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
