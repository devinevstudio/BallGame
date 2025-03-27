using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{

    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitSubState();
    }


    public override void CheckSwitchState()
    {
        if (!Ctx.Grounded)
        {
            SwitchState(Ctx.States.Falling());
        }
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void InitSubState()
    {
        //if moving => SetSubState(state.rolling)
        if(Ctx.PlayerRigidBody2D.linearVelocity.magnitude > 0) {
            SetSubState(Ctx.States.Rolling());
        }
        if (Ctx.PlayerRigidBody2D.linearVelocity.magnitude <= 0)
        {
            SetSubState(Ctx.States.Stopped());
        }
    }

    public override void Update()
    {
        CheckSwitchState();
        if (Ctx.IsPressed(PlayerStateMachine.ButtonType.JUMP))
        {
            Ctx.PlayerRigidBody2D.linearVelocityY += 5.0F;
            Ctx.ButtonHandled(PlayerStateMachine.ButtonType.JUMP);
        }
    }
}
