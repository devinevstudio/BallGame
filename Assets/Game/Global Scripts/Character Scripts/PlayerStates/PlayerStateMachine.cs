using System;
using System.Collections.Generic;
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
    public struct ButtonEvent
    {
        public ButtonEvent(ButtonType button, float duration)
        {
            Button = button;
            Duration = duration;
        }

        public ButtonType Button;
        public float Duration;

        public override string ToString() => $"({Button.ToString()}, {Duration})";
    }


    public enum ButtonType
    {
        JUMP,
        MOVE
    }

    //Keystates
    private bool _jumpPressed = false;
    private bool _cameraZoomedOut = false;

    //
    private bool _isGrounded;
    //private float _inputDamping;

    //State variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    private List<(ButtonEvent buttonEvent, float expirationDate)> _waitingEvents;


    //getters & setters

    public WorldScript WorldScript { get { return _worldScript; } }

    public CharacterController2D CharacterController { get { return _characterController; } }
    public PlayerCameraController CameraController { get { return _cameraController; } }

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public PlayerStateFactory States { get { return _states; } set { _states = value; } }


    public Rigidbody2D PlayerRigidBody2D { get { return _rigidBody2D; } }
    public Vector2 MoveInputVector { get { return _moveInputVector2; } }
    public bool Jumped { get { return _jumpPressed; } }
    public bool CameraZoomedOut { get { return _cameraZoomedOut; } }
    public bool Grounded { get { return _isGrounded; } }



    private void Awake()
    {
        _worldScript = GameObject.Find("WorldScript").GetComponent<WorldScript>();

        _cameraController = GameObject.Find("Main Camera").GetComponent<PlayerCameraController>();
        _characterController = GetComponent<CharacterController2D>();

        _waitingEvents = new List<(ButtonEvent, float)>();


        //Setup State
        _states = new PlayerStateFactory(this);

        _rigidBody2D = GetComponent<Rigidbody2D>();
        _moveInput = InputSystem.actions.FindAction("Move");
        _jumpInput = InputSystem.actions.FindAction("Jump");
        _CameraInput = InputSystem.actions.FindAction("CameraZoom");

        _moveInput.started += OnMoveStarted;
        _moveInput.performed += OnMovePerformed;
        _moveInput.canceled += OnMoveCanceled;

        _jumpInput.performed += OnJumpPerformed;
        _jumpInput.canceled += OnJumpCanceled;

        _CameraInput.performed += OnCameraZoomPerformed;
        _CameraInput.canceled += OnCameraZoomCanceled;
    }

    private void OnDestroy()
    {
        _moveInput.started -= OnMoveStarted;
        _moveInput.performed -= OnMovePerformed;
        _moveInput.canceled -= OnMoveCanceled;

        _jumpInput.performed -= OnJumpPerformed;
        _jumpInput.canceled -= OnJumpCanceled;

        _CameraInput.performed -= OnCameraZoomPerformed;
        _CameraInput.canceled -= OnCameraZoomCanceled;
    }

    private void OnCameraZoomPerformed(InputAction.CallbackContext context) {
        _cameraZoomedOut = (context.ReadValue<float>()) > 0;
        CameraController.ZoomedOut = _cameraZoomedOut;
        CameraController.UpdateZoom();
    }

    private void OnCameraZoomCanceled(InputAction.CallbackContext context) {
        _cameraZoomedOut = (context.ReadValue<float>()) > 0;
        CameraController.ZoomedOut = _cameraZoomedOut;
        CameraController.UpdateZoom();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context){
        _jumpPressed = (context.ReadValue<float>()) > 0;

        ButtonEvent jump = new ButtonEvent(ButtonType.JUMP, 1.0F);
        var tmp = _waitingEvents.Find(entry => entry.buttonEvent.Button == ButtonType.JUMP);
        if (tmp.Equals(default((ButtonEvent, float))))
        {
            _waitingEvents.Add((jump, Time.time + 1.0F));
        }
    }
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        _jumpPressed = (context.ReadValue<float>()) > 0;
        _waitingEvents.RemoveAll(entry => entry.buttonEvent.Button == ButtonType.JUMP);
    }

    private void OnMoveStarted(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    private void OnMovePerformed(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    private void OnMoveCanceled(InputAction.CallbackContext context) => _moveInputVector2 = context.ReadValue<Vector2>();

    void Start()
    {
        _currentState = _states.Idle();
        _characterController.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Update()
    {
        _currentState.UpdateStates();
        //_inputDamping = CharacterController.InputDamping;

        float currentTime = Time.time;
        _waitingEvents.RemoveAll(entry => entry.expirationDate <= currentTime);

        _isGrounded = CharacterController.Grounded;

        if (_characterController.Finished)
        {
            EnterNextLevel();
        }
    }

    public bool IsPressed(ButtonType key)
    {
        var tmp = _waitingEvents.Find(entry => entry.buttonEvent.Button == ButtonType.JUMP);
        if (!tmp.Equals(default((ButtonEvent, float))))return true;
        return false;
    }

    public void ButtonHandled(ButtonType key)
    {
        _waitingEvents.RemoveAll(entry => entry.buttonEvent.Button == key);
    }

    public void EnterNextLevel()
    {
        bool check = LevelManager.Instance.LoadNextScene();
        if (!check)
        {
            Debug.Log("CONGRATZ! YOU WON!!");
        }
    }
}
