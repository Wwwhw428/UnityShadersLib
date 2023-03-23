public class PlayerTouchingWallState : PlayerStates
{
    protected int inputX;
    protected bool jumpInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetAmountOfJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inputX = Player.InputHandler.InputX;
        jumpInput = Player.InputHandler.JumpInput;

        if (jumpInput)
        {
            StateMachine.ChangeState(Player.WallJumpState);
        }
    }
}
