using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        Ctx.PlayerRigidBody2D.gravityScale = 0;
    }




    public override void CheckSwitchState()
    {
        if (Ctx.Jumped)
        {
            Ctx.CharacterController.GetComponent<CircleCollider2D>().enabled = true;
            SwitchState(Ctx.States.Falling());
        }
        
    }

    public override void Enter()
    {
        Ctx.PlayerRigidBody2D.gravityScale = 0;
    }

    public override void Exit()
    {
        Ctx.PlayerRigidBody2D.gravityScale = 1;
    }

    public override void InitSubState()
    {
    }

    public override void Update()
    {
        Ctx.CharacterController.WaitForStart();
        CheckSwitchState();
    }
}
