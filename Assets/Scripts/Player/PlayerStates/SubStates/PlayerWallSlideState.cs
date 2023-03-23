using System.Collections;
using System.Collections.Generic;
using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    protected CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    private bool _isGrounded;
    public PlayerWallSlideState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!jumpInput)
        {
            if (Time.time - startTime >= PlayerData.WallSlideTime)
                StateMachine.ChangeState(Player.InAirState);
            else if (_isGrounded && Movement.CurrentVelocity.y == 0f)
                StateMachine.ChangeState(Player.IdleState);
            else
                Movement?.SetVelocityY(PlayerData.WallSlideVelocity * -1);
        }
    }

    public override void Docheck()
    {
        base.Docheck();
        _isGrounded = CollisionScene.Ground;
    }
}
