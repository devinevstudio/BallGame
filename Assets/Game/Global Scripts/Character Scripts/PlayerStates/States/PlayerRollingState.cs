using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

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
        Rigidbody2D rb = Ctx.PlayerRigidBody2D;



        if (Mathf.Abs(Ctx.MoveInputVector.x) > 0)
        {
            float multiplier = 1 / (1 + Mathf.Abs(Ctx.PlayerRigidBody2D.linearVelocity.magnitude));//decreases the multiplier the higher the magnitude is to prevent infinitely increasing the velocityX by holding left or right
            Ctx.PlayerRigidBody2D.linearVelocityX += (Ctx.MoveInputVector.x * multiplier) * 0.05F;
        }
    }
}
