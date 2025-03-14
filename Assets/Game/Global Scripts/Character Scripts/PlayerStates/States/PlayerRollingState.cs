using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{

    public PlayerRollingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void CheckSwitchState()
    {
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
