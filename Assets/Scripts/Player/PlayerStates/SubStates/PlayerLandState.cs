public class PlayerLandState : PlayerGroundStates
{
    public PlayerLandState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (animationFinished)
        {
            if (inputX != 0)
                StateMachine.ChangeState(Player.MoveState);
            else
                StateMachine.ChangeState(Player.IdleState);
        }
    }
}
