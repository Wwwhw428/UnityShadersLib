public class PlayerCrunchMoveState : PlayerGroundStates
{
    public PlayerCrunchMoveState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetColliderHeight(PlayerData.CrunchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        Player.SetColliderHeight(PlayerData.StandColliderHeigh);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (inputX == 0)
            StateMachine.ChangeState(Player.CrunchIdleState);
        else if (jumpInput)
        {
            Player.InputHandler.UseJumpInput();
        }
        else if (inputY > -0.01f && !isTouchingCeiling)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
        Movement?.CheckIfShouldFlip(inputX);
        Movement?.SetVelocityX(PlayerData.CrunchMovementVelocity * inputX);
    }
}
