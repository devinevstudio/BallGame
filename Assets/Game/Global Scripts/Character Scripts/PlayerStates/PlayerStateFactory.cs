public class PlayerStateFactory
{
    PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle() => new PlayerIdleState(_context, this);
    public PlayerBaseState Falling() => new PlayerFallingState(_context, this);
    public PlayerBaseState Rolling() => new PlayerRollingState(_context, this);
    public PlayerBaseState Grounded() => new PlayerGroundedState(_context, this);
    public PlayerBaseState Stopped() => new PlayerStoppedState(_context, this);
}
