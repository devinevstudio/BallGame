using UnityEngine;

public class PlayerStoppedState : PlayerBaseState
{

    public PlayerStoppedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchState()
    {
        //if move input -> switchstate to rolling.
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void InitSubState()
    {
        
    }

    public override void Update()
    {
        CheckSwitchState();
    }
}
