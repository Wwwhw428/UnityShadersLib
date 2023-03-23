using MyCell.CoreSystem.CoreComponent;
using UnityEngine;
public class PlayerGroundStates : PlayerStates
{
    protected CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    protected int inputX;
    protected int inputY;
    protected bool jumpInput;
    protected bool isGrounded;
    protected bool isTouchingCeiling;

    public PlayerGroundStates(Player player, PlayerStateMachine statesMachine, PlayerData_SO playerData, string animBoolName) : base(player, statesMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.JumpState.ResetAmountOfJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputX = Player.InputHandler.InputX;
        inputY = Player.InputHandler.InputY;
        jumpInput = Player.InputHandler.JumpInput;

        if (Player.InputHandler.AttackInput[(int)CombatInput.primary] && !isTouchingCeiling)
        {
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }
        else if (Player.InputHandler.AttackInput[(int)CombatInput.secondary] && !isTouchingCeiling)
        {
            StateMachine.ChangeState(Player.SecondaryAttackState);
        }
        else if (inputY >= 0 && jumpInput)
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!isGrounded)
        {
            StateMachine.ChangeState(Player.InAirState);
        }
    }

    public override void Docheck()
    {
        base.Docheck();
        isGrounded = CollisionScene.Ground;
        isTouchingCeiling = CollisionScene.Ceiling;
    }
}
