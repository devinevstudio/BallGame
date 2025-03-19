using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Video;

public abstract class PlayerBaseState
{

    private PlayerStateMachine _ctx;
    private PlayerStateFactory _factory;

    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;

    private bool _isRootState = false;
    
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected PlayerStateFactory Factory { get { return _factory; } }
    protected PlayerBaseState CurrentSubState { get { return _currentSubState; } }
    protected PlayerBaseState CurrentSuperState { get { return _currentSuperState; } }
    protected bool IsRootState { set { _isRootState = value; } }

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }


    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public abstract void CheckSwitchState();
    public abstract void InitSubState();


    public void UpdateStates() {
        Update();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    public void ExitStates()
    {
        Exit();
        if(_currentSubState != null)
        {
            _currentSubState.ExitStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState) {
        Exit();
        newState.Enter();

        if(_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if(_currentSuperState!=null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(PlayerBaseState newSuperState) {
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
