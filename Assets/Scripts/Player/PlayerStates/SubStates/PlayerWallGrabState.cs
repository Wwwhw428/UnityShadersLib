using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{

    public PlayerWallGrabState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocity(0, 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!jumpInput)
        {
            if (Time.time - startTime >= PlayerData.WallGrabTime)
                StateMachine.ChangeState(Player.WallSlideState);
            else
                Movement?.SetVelocity(0, 0);
        }
    }
}
