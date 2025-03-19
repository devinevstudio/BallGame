using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{

    /*
    
        Initially thought of this state just being falling, but since this is a simple ball physics game, 
        there is no need to have different classes for falling and jumping/flying, so the Falling State can
        also be seen as Air State

    */

    public PlayerFallingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitSubState();
    }




    public override void CheckSwitchState()
    {
        if (Ctx.Grounded)
        {
            SwitchState(Ctx.States.Grounded());
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
    }

    public override void Update()
    {
        CheckSwitchState();
        if(Ctx.MoveInputVector.x != 0)
        {
            float multiplier = 1 / (1 + Mathf.Abs(Ctx.PlayerRigidBody2D.linearVelocity.magnitude));//decreases the multiplier the higher the magnitude is to prevent infinitely increasing the velocityX by holding left or right
            Ctx.PlayerRigidBody2D.linearVelocityX += (Ctx.MoveInputVector.x * multiplier)*0.05F;
        }
    }
}
