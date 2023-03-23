public class PlayerMoveState : PlayerGroundStates
{
    public PlayerMoveState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (inputX == 0)
            StateMachine.ChangeState(Player.IdleState);
        else if (inputY < 0)
            StateMachine.ChangeState(Player.CrunchMoveState);
        Movement?.CheckIfShouldFlip(inputX);
        Movement?.SetVelocityX(PlayerData.MovementVelocity * inputX);
    }
}
