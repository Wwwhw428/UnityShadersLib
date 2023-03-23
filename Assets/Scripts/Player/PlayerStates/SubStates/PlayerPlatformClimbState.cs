using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformClimbState : PlayerGroundStates
{
    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _cornerPos;
    public PlayerPlatformClimbState(Player player, PlayerStateMachine stateMachine, PlayerData_SO playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocity(0, 0);
        DetermineCornerPos();
        _startPos.Set(
            _cornerPos.x + PlayerData.StartOffset.x * Movement.CurrentFaceDirection,
            _cornerPos.y + PlayerData.StartOffset.y);
        _endPos.Set(
            _cornerPos.x + PlayerData.EndOffset.x * Movement.CurrentFaceDirection,
            _cornerPos.y + PlayerData.EndOffset.y);
        Player.transform.position = _startPos;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    private void DetermineCornerPos()
    {
        
    }

}
