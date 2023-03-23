using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _jumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        if (Player.JumpState.AmountOfJumpLeft > 0)
        {
            _jumpDirection = Movement.CurrentFaceDirection * -1;
            Movement?.CheckIfShouldFlip(_jumpDirection);
            Movement?.SetVelocity(PlayerData.JumpVelocity, PlayerData.WallJumpAngle, _jumpDirection);
            Player.JumpState.DecreaseAmountOfJump();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

		Player.Anim.SetFloat("XVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        Player.Anim.SetFloat("YVelocity", Movement.CurrentVelocity.y);

        if (Time.time - startTime >= PlayerData.WallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}
