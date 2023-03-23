using System;
using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

public class PlayerInAirState : PlayerStates
{
    protected CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    private bool _isGrounded;
    private bool _isWalled;
    private bool _isTouchingLedged;
    private int _inputX;
    private int _inputY;
    private bool _jumpInput;

    private float _startTime;

    public PlayerInAirState(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _startTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time - _startTime > 0.06)
            Player.GetComponent<BoxCollider2D>().isTrigger = false;

        _inputX = Player.InputHandler.InputX;
        _inputY = Player.InputHandler.InputY;
        _jumpInput = Player.InputHandler.JumpInput;

        Player.InputHandler.UseJumpInput();

        if (_jumpInput && Player.JumpState.AmountOfJumpLeft > 0)
            StateMachine.ChangeState(Player.JumpState);

        if (_isWalled && _inputX == Movement?.CurrentFaceDirection && _inputY > 0)
        {
            if (!_isTouchingLedged)
                StateMachine.ChangeState(Player.LedgeClimbState);
            else if (_inputY < 0f)
                StateMachine.ChangeState(Player.WallGrabState);
            else
                StateMachine.ChangeState(Player.WallClimbState);
        }
        else if (_isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else
        {
            Movement?.CheckIfShouldFlip(_inputX);
            Movement?.SetVelocityX(PlayerData.MovementVelocity * _inputX);
            Player.Anim.SetFloat("XVelocity", Math.Abs(Movement.CurrentVelocity.x));
            Player.Anim.SetFloat("YVelocity", Movement.CurrentVelocity.y);
        }
    }

    public override void Docheck()
    {
        base.Docheck();
        _isWalled = CollisionScene.WallFront;
        _isGrounded = CollisionScene.Ground;
        _isTouchingLedged = CollisionScene.LedgeHorizontal;
    }
}
