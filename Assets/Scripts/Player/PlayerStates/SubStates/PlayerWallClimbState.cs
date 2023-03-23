using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    private CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    private bool _isLedge;

    public PlayerWallClimbState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Docheck()
    {
        base.Docheck();
        _isLedge = CollisionScene.LedgeHorizontal;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isLedge)
        {
            StateMachine.ChangeState(Player.LedgeClimbState);
        }
        else if (!jumpInput)
        {

            if (Time.time - startTime >= PlayerData.WallClimbTime)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else
            {
                Movement?.SetVelocityY(PlayerData.WallClimbVelocity);
            }
        }
    }
}
