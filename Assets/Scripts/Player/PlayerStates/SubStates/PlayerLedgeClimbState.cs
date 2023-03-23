using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerStates
{
    protected CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    private Vector2 _vector2WorkSpace;
    private Vector2 _cornerPos;
    private Vector2 _startPos;
    private Vector2 _endPos;
    private bool _isHanging;
    private bool _isClimbing;
    private int _inputY;
    private bool _isTouchingCeiling;
    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData_SO playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        _isHanging = true;
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        Player.Anim.SetBool("LedgeClimb", false);
    }
    // TODO: ledgeClimb->Idle����������
    // TODO: InAir->ledgeClimb cornerPos���ܻ����
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
        Player.transform.position = _endPos;
        _isHanging = false;
        _isClimbing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Player.transform.position = _cornerPos + PlayerData.StartOffset;

        if (animationFinished)
        {
            if (_isTouchingCeiling)
                StateMachine.ChangeState(Player.CrunchIdleState);
            else
                StateMachine.ChangeState(Player.IdleState);
        }
        else
        {
            _inputY = Player.InputHandler.InputY;
            Player.transform.position = _startPos;
            Movement?.SetVelocity(0, 0);
            if (_inputY > 0f && _isHanging && !_isClimbing)
            {
                CheckCeiling();
                Player.Anim.SetBool("LedgeClimb", true);
                _isClimbing = true;
            }
        }

    }

    public void CheckCeiling()
    {
        //_isTouchingCeiling = Physics2D.Raycast(
        //    player.ConerPos + Vector2.up * 0.015f + Vector2.right * Movement.CurrentFaceDirection * 0.015f,
        //    Vector2.up, playerData.CeilingCheckRadius, playerData.WhatIsGround);
        _isTouchingCeiling = Physics2D.OverlapCircle(
            _endPos, PlayerData.CeilingCheckRadius, PlayerData.WhatIsGround
            );
    }

    // TODO: CornerPos �����и��Ƶ���ʱ���ǲ�׼
    public void DetermineCornerPos()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionScene.WallCheck.position, Vector2.right * Movement.CurrentFaceDirection, CollisionScene.WallCheckDistance, CollisionScene.WhatIsGround);

        float xDistance = xHit.distance * Movement.CurrentFaceDirection;

        float yCheckDistance = CollisionScene.LedgeCheckHorizontal.position.y - CollisionScene.WallCheck.position.y;
        _vector2WorkSpace.Set(CollisionScene.LedgeCheckHorizontal.position.x + xDistance, CollisionScene.LedgeCheckHorizontal.position.y);

        RaycastHit2D yHit = Physics2D.Raycast((Vector3)_vector2WorkSpace, Vector2.down, yCheckDistance, CollisionScene.WhatIsGround);
        float yDistance = CollisionScene.LedgeCheckHorizontal.position.y - CollisionScene.WallCheck.position.y - yHit.distance;

        _vector2WorkSpace.Set(CollisionScene.WallCheck.position.x + xDistance, CollisionScene.WallCheck.position.y + yDistance);

        _cornerPos = _vector2WorkSpace;
    }
}
