public class PlayerJumpState : PlayerAbilityState
{
    public int AmountOfJumpLeft { get; private set; }
    public PlayerJumpState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
        AmountOfJumpLeft = playerData.AmountOfJump;
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        if (AmountOfJumpLeft > 0)
        {
            Movement?.SetVelocityY(PlayerData.JumpVelocity);
            AmountOfJumpLeft--;
        }
        isAbilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public void ResetAmountOfJump() => AmountOfJumpLeft = PlayerData.AmountOfJump;

    public void DecreaseAmountOfJump() => AmountOfJumpLeft--;

}
