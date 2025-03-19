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

        

        //TODO: Work on it, when time
        //Vector2 moveInput = Ctx.MoveInputVector;
        //if (Mathf.Sign(moveInput.x) != Mathf.Sign(rb.linearVelocityX))
        //{
        //    rb.linearVelocityX += moveInput.x * 0.25F; //Deaccellerating Ball

        //}
        //else
        //{
        //    rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX + moveInput.x, -20.0F, 20.0F) * Ctx.CharacterController.InputDamping; //Accelerating Ball
        //}
    }
}
