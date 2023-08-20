using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField]
    private float _runSpeed = 6.0f;
    [SerializeField]
    private float _walkSpeed = 3.0f;
    [SerializeField]
    private float _rotationSpeed = 5f;

    //State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    public CharacterController controller;
    public PlayerInput playerInput;
    [SerializeField] private Animator _animator;
    private PlayerRayDetection _playerRayDetection;

    int _isWalkingHash;
    int _isRunningHash;
    int _isCarryingHash;
    int _isDancingHash;
    int _isJumpingHash;

    [SerializeField] private bool _isRunPressed;
    public bool isInteractPressed;
    [SerializeField] private bool _isMovementPressed;

    private bool _isDancing;
    [SerializeField] float _danceTimerTime = 15.0f;
    [SerializeField] float _currentTimer = 0.0f;
    private bool _startDanceTimer;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;

    //Gravity variables
    float _gravity = -9.8f;
    float _groundedGravity = -0.05f;

    //Jumping variables
    bool _isJumpPressed = false;
    float _initialJumpVelocity;
    float _maxJumpHeight = 4.0f;
    float _maxJumpTime = .75f;
    bool _isJumping = false;
    int _jumpCountHash;
    bool _requireNewJumpPress = false;
    int _jumpCount = 0;
    Dictionary<int, float> _initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
    Coroutine _currentJumpResetRoutine = null;

    //carrying variables
    public bool isCarryingNow;
    public bool isPushingTrolleyNow;
    public bool isOnPickupZone;
    public bool isOnTrolleyZone;
    public GameObject itemOnCarry;
    [SerializeField] Transform itemPos;

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Animator Animator { get { return _animator; } }
    public CharacterController CharacterController { get { return controller; } }
    public Coroutine CurrentJumpResetRoutine { get { return _currentJumpResetRoutine; } set { _currentJumpResetRoutine = value; } }
    public Dictionary<int, float> InitialJumpVelocities { get { return _initialJumpVelocities; } }
    public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
    public int JumpCount { get { return _jumpCount; } set { _jumpCount = value; } }
    public int IsWalkingHash { get { return _isWalkingHash; } }
    public int IsRunningHash { get { return _isRunningHash; } }
    public int IsJumpingHash { get { return _isJumpingHash; } }
    public int JumpCountHash { get { return _jumpCountHash; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }
    public bool IsJumping { set { _isJumping = value; } }
    public bool IsJumpPressed { get { return _isJumpPressed; }}
    public float GroundedGravity { get { return _groundedGravity; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return _appliedMovement.z; } set { _appliedMovement.z = value; } }
    public float RunMultiplier { get { return _runSpeed; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }

    private void Awake()
    {
        // set reference variables
        playerInput = new PlayerInput();
        controller = GetComponent<CharacterController>();
        _playerRayDetection = GetComponent<PlayerRayDetection>();
        //playerInput = GetComponent<PlayerInput>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // set hash reference
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _jumpCountHash = Animator.StringToHash("jumpCount");
        _isCarryingHash = Animator.StringToHash("isCarrying");
        _isDancingHash = Animator.StringToHash("isDancing");

        // set the player input callbacks
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += ctx => _isRunPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Run.canceled += ctx => _isRunPressed = ctx.ReadValueAsButton();
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;
        playerInput.CharacterControls.Drop.started += onDrop;
        playerInput.CharacterControls.Interact.started += onInteract;
        playerInput.CharacterControls.Interact.canceled += onInteract;

        SetupJumpVariables();
    }
    void Start()
    {

    }

    void Update()
    {
        HandleRotation();
        _currentState.UpdateStates();
        MoveController();
        HandleDanceTimer();
    }

    void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (_maxJumpHeight + 2)) / Mathf.Pow((timeToApex * 1.25f), 2);
        float secondJumpInitialVelocity = (2 * (_maxJumpHeight + 2)) / (timeToApex * 1.25f);
        float thirdJumpGravity = (-2 * (_maxJumpHeight + 4)) / Mathf.Pow((timeToApex * 1.5f), 2);
        float thirdJumpInitialVelocity = (2 * (_maxJumpHeight + 4)) / (timeToApex * 1.5f);

        _initialJumpVelocities.Add(1, _initialJumpVelocity);
        _initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        _initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        _jumpGravities.Add(0, _gravity);
        _jumpGravities.Add(1, _gravity);
        _jumpGravities.Add(2, secondJumpGravity);
        _jumpGravities.Add(3, thirdJumpGravity);
    }

    private void MoveController()
    {
        if (_isRunPressed)
        {
            _appliedMovement.x = _currentMovement.x * _runSpeed;
            _appliedMovement.z = _currentMovement.z * _runSpeed;
        }
        else
        {
            _appliedMovement.x = _currentMovement.x * _walkSpeed;
            _appliedMovement.z = _currentMovement.z * _walkSpeed;
        }

        controller.Move(_appliedMovement * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = _currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void onMovementInput(InputAction.CallbackContext ctx)
    {
        _currentMovementInput = ctx.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        if (!_isMovementPressed && !_isDancing && !isCarryingNow && !isPushingTrolleyNow)
        {
            StartDanceCountDown();
        }
        else if (_isMovementPressed)
        {
            _isDancing = false;
            _startDanceTimer = false;
        }
    }
    private void onJump(InputAction.CallbackContext ctx)
    {
        _isJumpPressed = ctx.ReadValueAsButton();
        _requireNewJumpPress = false;
    }

    private void onInteract(InputAction.CallbackContext ctx)
    {
        isInteractPressed = ctx.ReadValueAsButton();
        _isDancing = false;
    }
    private void onDrop(InputAction.CallbackContext ctx)
    {
        if (isCarryingNow && !isOnPickupZone && !isOnTrolleyZone)
        {
            Debug.Log("Dropped the item");
            DropItem();
        }
    }

    void DropItem()
    {
        isCarryingNow = false;
        Destroy(itemPos.GetChild(0).gameObject);

        Vector3 GroundOffset = transform.forward - new Vector3(-0.0f, 0.0f, -0.4f);
        GameObject newGroundObject = Instantiate(itemOnCarry, transform.position + GroundOffset, transform.rotation, null);

        BoxCollider itemsCollider = newGroundObject.GetComponent<BoxCollider>();
        if (itemsCollider != null) { itemsCollider.isTrigger = true; }

        newGroundObject.transform.localScale = new Vector3(0.6f, 0.15f, 0.3f);
        itemOnCarry = null;
        isInteractPressed = false;
    }

    public void RemoveItemFromHand()
    {
        isInteractPressed = false;
        isCarryingNow = false;
        Destroy(itemPos.GetChild(0).gameObject);
        itemOnCarry = null;
    }

    public void AddItemToHand(GameObject item)
    {
        isInteractPressed = false;
        GameObject itemInHand = Instantiate(item, itemPos.position, itemPos.rotation, itemPos);
        BoxCollider itemsCollider = itemInHand.GetComponent<BoxCollider>();
        if (itemsCollider != null)
        {
            itemsCollider.enabled = false;
        }
    }

    private void StartDanceCountDown()
    {
        _currentTimer = _danceTimerTime;
        _startDanceTimer = true;
    }
    void HandleDanceTimer()
    {
        if (_startDanceTimer)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0.0f)
            {
                _isDancing = true;
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
