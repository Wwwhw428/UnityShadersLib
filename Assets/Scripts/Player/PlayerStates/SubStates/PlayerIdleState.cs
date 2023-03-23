public class PlayerIdleState : PlayerGroundStates
{
    public PlayerIdleState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (inputX != 0)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
        else if(inputY < 0)
        {
            StateMachine.ChangeState(Player.CrunchIdleState);
        }
    }
}
