using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{

    private WorldScript _worldScript;

    private Rigidbody2D _rigidBody2D;

    CharacterController2D _characterController;
    PlayerCameraController _cameraController;

    private InputAction _moveInput;
    private InputAction _jumpInput;
    private InputAction _CameraInput;


    private Vector2 _moveInputVector2;


    
    //Keystates
    private bool _jumpPressed = false;
    private bool _cameraZoomedOut = false;

    //
    private bool _isGrounded;

    //State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //getters & setters

    public WorldScript WorldScript { get { return _worldScript; } }

    public CharacterController2D CharacterController { get { return _characterController; } }
    public PlayerCameraController CameraController { get { return _cameraController; } }

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public PlayerStateFactory States { get { return _states; } set { _states = value; } }

    public bool Collided { get { return (_characterController.CollisionCount > 0); } }


    public Rigidbody2D PlayerRigidBody2D { get { return _rigidBody2D; } }
    public Vector2 MoveInputVector { get { return _moveInputVector2; } }
    public bool Jumped{ get { return _jumpPressed; } }
    public bool CameraZoomedOut{ get { return _cameraZoomedOut; } }
    public bool Grounded { get { return _isGrounded; } }
    

    private void Awake()
    {

        _worldScript = GameObject.Find("WorldScript").GetComponent<WorldScript>();

        _cameraController = GameObject.Find("Main Camera").GetComponent<PlayerCameraController>();
        _characterController = GetComponent<CharacterController2D>();

        //Setup State
        _states = new PlayerStateFactory(this);

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _moveInput = InputSystem.actions.FindAction("Move");
        _jumpInput = InputSystem.actions.FindAction("Jump");
        _CameraInput = InputSystem.actions.FindAction("Camera");

        _moveInput.started += OnMoveStarted;
        _moveInput.performed += OnMovePerformed;
        _moveInput.canceled += OnMoveCanceled;

        _jumpInput.performed += OnJumpPerformed;
        _jumpInput.canceled += OnJumpCanceled;

        _CameraInput.performed += OnCameraZoomPerformed;
        _CameraInput.canceled += OnCameraZoomCanceled;
    }

    private void OnCameraZoomPerformed(InputAction.CallbackContext context) {
        _cameraZoomedOut = (context.ReadValue<float>()) > 0;
        CameraController.ZoomedOut = _cameraZoomedOut;
        CameraController.UpdateZoom();
    }

    private void OnCameraZoomCanceled(InputAction.CallbackContext context){
        _cameraZoomedOut = (context.ReadValue<float>())>0;
        CameraController.ZoomedOut = _cameraZoomedOut;
        CameraController.UpdateZoom();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) => _jumpPressed = (context.ReadValue<float>()) > 0;

    private void OnJumpCanceled(InputAction.CallbackContext context) => _jumpPressed = (context.ReadValue<float>())>0;

    private void OnMoveStarted(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    private void OnMovePerformed(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    private void OnMoveCanceled(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    void Start()
    {
        _currentState = _states.Idle();
    }

    void Update()
    {
        _currentState.UpdateStates();
        _isGrounded = CharacterController.Grounded;

        if (_characterController.Finished)
        {
            _currentState = _states.Idle();
            _characterController.ResetPosition();
            _characterController.Finished = false;
        }
    }
}
